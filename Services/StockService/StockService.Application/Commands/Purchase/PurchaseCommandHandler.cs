using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using StockService.Domain.Models.Repositories;
using StockService.Domain.Models.Entities;
using Framework.Core.DomainObjects;
using MediatR;
using StockService.Domain.Rules;


namespace StockService.Application.Commands.Purchase
{
    public class PurchaseCommandHandler : CommandHandler<PurchaseCommand, PurchaseCommandOutput, PurchaseCommandValidation>

    {
        private readonly ITransactionStockRepository _transactionStockRepository;
        private readonly IStockResultTransactionStockRepository _stockResultTransactionStockRepository;
        private readonly IStockRepository _stockRepository;

        public PurchaseCommandHandler(ITransactionStockRepository transactiontRepository,
                                      IStockResultTransactionStockRepository stockResultTransactionStockRepository,
                                      IStockRepository stockRepository,
                                      IDomainNotification domainNotification,
                                      IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _transactionStockRepository = transactiontRepository;
            _stockRepository = stockRepository;
            _stockResultTransactionStockRepository = stockResultTransactionStockRepository;
        }

        public override async Task<PurchaseCommandOutput> ExecutCommand(PurchaseCommand request, CancellationToken cancellationToken)
        {
            _domainNotification.AddNotification(await CheckValidations(request));
            if (_domainNotification.HasNotifications) return request.GetCommandOutput();

            var stock = await _stockRepository.GetBySymbol(request.Symbol);

            var transaction = TransactionStock.Purchase(request.Amount,
                                                   request.Value,
                                                   stock.StockId,
                                                   request.InvestmentDate,
                                                   request.CorrelationId);

            _transactionStockRepository.Add(transaction);

            var stockResultTransaction = await _stockResultTransactionStockRepository.GetByStockId(stock.StockId);
            var existsStockResultTransaction = stockResultTransaction != null;

            stockResultTransaction ??= StockResultTransaction.Create(stock.StockId,request.CorrelationId);
            stockResultTransaction.Add(request.Amount, request.Value, request.CorrelationId);
            if(!existsStockResultTransaction){
                _stockResultTransactionStockRepository.Add(stockResultTransaction);
            }else{
                _stockResultTransactionStockRepository.Update(stockResultTransaction);
            }

            await PersistData(_transactionStockRepository.UnitOfWork);

            return new PurchaseCommandOutput
            {
                TransactionStockId = transaction.TransactionStockId.Value,
                Amount = transaction.Amount,
                Value = transaction.Value,
                Symbol = request.Symbol,
                InvestmentDate = transaction.InvestmentDate
            };


        }
        public async Task<List<NotificationMessage>> CheckValidations(PurchaseCommand request)
        {
            var lstNotifications = new List<NotificationMessage>();

            lstNotifications.AddRange(await BusinessRuleValidation.Check(new StockExistsRule(request.Symbol, _stockRepository)));
            return lstNotifications;
        }
    }
}
