using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using StockService.Domain.Models.Repositories;
using StockService.Domain.Models.Entities;


namespace StockService.Application.Commands.Purchase
{
    public class PurchaseCommandHandler : CommandHandler<PurchaseCommand, PurchaseCommandOutput, PurchaseCommandValidation>

    {
        private readonly ITransactionRepository _transactiontRepository;

        public PurchaseCommandHandler(ITransactionRepository transactiontRepository,
                                         IDomainNotification domainNotification,
                                         IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _transactiontRepository = transactiontRepository;
        }

        public override async Task<PurchaseCommandOutput> ExecutCommand(PurchaseCommand request, CancellationToken cancellationToken)
        {
            var transaction = Transaction.Purchase(request.Amount, request.Value, request.Symbol, request.InvestmentDate, request.CorrelationId);

            _transactiontRepository.Add(transaction);

            await PersistData(_transactiontRepository.UnitOfWork);

            return new PurchaseCommandOutput
            {
                TransactionId = transaction.AggregateId,
                Amount = transaction.Amount,
                Value = transaction.Value,
                Symbol = transaction.Symbol,
                InvestmentDate = transaction.InvestmentDate
            };
        }
    }
}
