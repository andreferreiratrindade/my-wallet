using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using StockService.Domain.Models.Repositories;
using StockService.Domain.Models.Entities;
using Framework.Core.DomainObjects;
using Activities.Domain.Rules;
using MediatR;


namespace StockService.Application.Commands.Sell
{
    public class SellCommandHandler : CommandHandler<SellCommand, SellCommandOutput, SellCommandValidation>

    {
        private readonly ITransactionRepository _transactiontRepository;
        private readonly IStockResultTransactionRepository _stockResultTransactionRepository;
        private readonly IStockRepository _stockRepository;

        public SellCommandHandler(ITransactionRepository transactiontRepository,
                                      IStockResultTransactionRepository stockResultTransactionRepository,
                                      IStockRepository stockRepository,
                                      IDomainNotification domainNotification,
                                      IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _transactiontRepository = transactiontRepository;
            _stockRepository = stockRepository;
            _stockResultTransactionRepository = stockResultTransactionRepository;
        }

        public override async Task<SellCommandOutput> ExecutCommand(SellCommand request, CancellationToken cancellationToken)
        {
            _domainNotification.AddNotification(await CheckValidations(request));
            if (_domainNotification.HasNotifications) return request.GetCommandOutput();

            var stock = await _stockRepository.GetBySymbol(request.Symbol);

            var transaction = Transaction.Sell(request.Amount,
                                                   request.Value,
                                                   stock.StockId,
                                                   request.InvestmentDate,
                                                   request.CorrelationId);

            _transactiontRepository.Add(transaction);

            var stockResultTransaction = await _stockResultTransactionRepository.GetByStockId(stock.StockId);
            var existsStockResultTransaction = stockResultTransaction != null;

            stockResultTransaction ??= StockResultTransaction.Create(stock.StockId,request.CorrelationId);
            stockResultTransaction.Decrease(request.Amount, request.Value, request.CorrelationId);
            if(!existsStockResultTransaction){
                _stockResultTransactionRepository.Add(stockResultTransaction);
            }else{
                _stockResultTransactionRepository.Update(stockResultTransaction);
            }

            await PersistData(_transactiontRepository.UnitOfWork);

            return new SellCommandOutput
            {
                TransactionId = transaction.TransactionId.Value,
                Amount = transaction.Amount,
                Value = transaction.Value,
                Symbol = request.Symbol,
                InvestmentDate = transaction.InvestmentDate
            };


        }
        public  async  Task<List<NotificationMessage>> CheckValidations(SellCommand request)
        {

            List<NotificationMessage> lstNotifications = new();

            lstNotifications.AddRange( await BusinessRuleValidation.Check(new StockExistsRule(request.Symbol, _stockRepository)));
            lstNotifications.AddRange( await BusinessRuleValidation.Check(new HasStockAmountEnoughToSellRule(request.Symbol, request.Amount, _stockRepository, _stockResultTransactionRepository)));
            return lstNotifications;
        }
    }
}
