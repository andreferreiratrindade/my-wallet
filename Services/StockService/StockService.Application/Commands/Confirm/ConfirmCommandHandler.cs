using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using StockService.Domain.Models.Repositories;


namespace StockService.Application.Commands.Purchase
{
    public class ConfirmCommandHandler : CommandHandler<ConfirmCommand, ConfirmCommandOutput, ConfirmCommandValidation>

    {
        private readonly ITransactionStockRepository _transactionStockRepository;

        public ConfirmCommandHandler(ITransactionStockRepository transactiontRepository,

                                      IDomainNotification domainNotification,
                                      IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _transactionStockRepository = transactiontRepository;
        }

        public override async Task<ConfirmCommandOutput> ExecutCommand(ConfirmCommand request, CancellationToken cancellationToken)
        {

            var transactionStock = await _transactionStockRepository.GetById(request.TransactionStockId);
            transactionStock.Confirm(request.CorrelationId);
           _transactionStockRepository.Update(transactionStock);

            await PersistData(_transactionStockRepository.UnitOfWork);

            return new ConfirmCommandOutput
            {
                TransactionStockId = transactionStock.TransactionStockId,
            };


        }
        public async Task<List<NotificationMessage>> CheckValidations(PurchaseCommand request)
        {
            var lstNotifications = new List<NotificationMessage>();


            return lstNotifications;
        }
    }
}
