using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using StockService.Domain.Models.Repositories;
using StockService.Domain.Models.Entities;
using Framework.Core.DomainObjects;
using Activities.Domain.Rules;
using MediatR;


namespace StockService.Application.Commands.Purchase
{
    public class PurchaseCommandHandler : CommandHandler<PurchaseCommand, PurchaseCommandOutput, PurchaseCommandValidation>

    {
        private readonly ITransactionRepository _transactiontRepository;
        private readonly IStockResultTransactionRepository _stockResultTransactionRepository;
        private readonly IStockRepository _stockRepository;

        public PurchaseCommandHandler(ITransactionRepository transactiontRepository,
                                      IStockResultTransactionRepository stockResultTransactionRepository,
                                      IStockRepository stockRepository,
                                      IDomainNotification domainNotification,
                                      IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _transactiontRepository = transactiontRepository;
            _stockRepository = stockRepository;
            _stockResultTransactionRepository = stockResultTransactionRepository;
        }

        public override async Task<PurchaseCommandOutput> ExecutCommand(PurchaseCommand request, CancellationToken cancellationToken)
        {
            _domainNotification.AddNotification(CheckValidations(request));
            if (_domainNotification.HasNotifications) return request.GetCommandOutput();

            var stock = await _stockRepository.GetBySymbol(request.Symbol);

            var transaction = Transaction.Purchase(request.Amount,
                                                   request.Value,
                                                   stock.StockId,
                                                   request.InvestmentDate,
                                                   request.CorrelationId);

            _transactiontRepository.Add(transaction);

            var stockResultTransaction = await _stockResultTransactionRepository.GetByStockId(stock.StockId);
            var existsStockResultTransaction = stockResultTransaction != null;

            stockResultTransaction ??= StockResultTransaction.Create(stock.StockId,request.CorrelationId);
            stockResultTransaction.Add(request.Amount, request.Value, request.CorrelationId);
            if(!existsStockResultTransaction){
                _stockResultTransactionRepository.Add(stockResultTransaction);
            }else{
                _stockResultTransactionRepository.Update(stockResultTransaction);
            }

            await PersistData(_transactiontRepository.UnitOfWork);

            return new PurchaseCommandOutput
            {
                TransactionId = transaction.TransactionId.Value,
                Amount = transaction.Amount,
                Value = transaction.Value,
                Symbol = request.Symbol,
                InvestmentDate = transaction.InvestmentDate
            };


        }
        public List<NotificationMessage> CheckValidations(PurchaseCommand request)
        {
            var lstNotifications = new List<NotificationMessage>();

            lstNotifications.AddRange(BusinessRuleValidation.Check(new StockExistsRule(request.Symbol, _stockRepository)));
            return lstNotifications;
        }
    }
}
