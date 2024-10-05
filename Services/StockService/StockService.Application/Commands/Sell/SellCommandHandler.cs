using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using StockService.Domain.Models.Repositories;
using StockService.Domain.Models.Entities;
using Framework.Core.DomainObjects;
using MediatR;
using StockService.Domain.Rules;


namespace StockService.Application.Commands.Sell
{
    public class SellCommandHandler : CommandHandler<SellCommand, SellCommandOutput, SellCommandValidation>

    {
        private readonly ITransactionStockRepository _transactionStockRepository;
        private readonly IStockResultTransactionStockRepository _stockResultTransactionStockRepository;
        private readonly IStockRepository _stockRepository;

        public SellCommandHandler(ITransactionStockRepository transactiontRepository,
                                      IStockResultTransactionStockRepository stockResultTransactionStockRepository,
                                      IStockRepository stockRepository,
                                      IDomainNotification domainNotification,
                                      IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _transactionStockRepository = transactiontRepository;
            _stockRepository = stockRepository;
            _stockResultTransactionStockRepository = stockResultTransactionStockRepository;
        }

        public override async Task<SellCommandOutput> ExecutCommand(SellCommand request, CancellationToken cancellationToken)
        {
            _domainNotification.AddNotification(await CheckValidations(request));
            if (_domainNotification.HasNotifications) return request.GetCommandOutput();

            var stock = await _stockRepository.GetBySymbol(request.Symbol);

            var transaction = TransactionStock.Sell(request.Amount,
                                                   request.Value,
                                                   stock.StockId,
                                                   request.InvestmentDate,
                                                   request.CorrelationId);

            _transactionStockRepository.Add(transaction);

            var stockResultTransaction = await _stockResultTransactionStockRepository.GetByStockId(stock.StockId);
            stockResultTransaction.Decrease(request.Amount, request.Value, request.CorrelationId);

            _stockResultTransactionStockRepository.Update(stockResultTransaction);

            await PersistData(_transactionStockRepository.UnitOfWork);

            return new SellCommandOutput
            {
                TransactionStockId = transaction.TransactionStockId.Value,
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
            lstNotifications.AddRange( await BusinessRuleValidation.Check(new HasStockAmountEnoughToSellRule(request.Symbol, request.Amount, _stockRepository, _stockResultTransactionStockRepository)));
            return lstNotifications;
        }
    }
}
