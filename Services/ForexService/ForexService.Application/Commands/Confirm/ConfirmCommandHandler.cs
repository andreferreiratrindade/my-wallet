using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using ForexService.Domain.Models.Repositories;


namespace ForexService.Application.Commands.Purchase
{
    public class ConfirmCommandHandler : CommandHandler<ConfirmCommand, ConfirmCommandOutput, ConfirmCommandValidation>

    {
        private readonly ITransactionForexRepository _transactionForexRepository;

        public ConfirmCommandHandler(ITransactionForexRepository transactiontRepository,

                                      IDomainNotification domainNotification,
                                      IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _transactionForexRepository = transactiontRepository;
        }

        public override async Task<ConfirmCommandOutput> ExecutCommand(ConfirmCommand request, CancellationToken cancellationToken)
        {

            var transactionForex = await _transactionForexRepository.GetById(request.TransactionForexId);
            transactionForex.Confirm(request.CorrelationId);
           _transactionForexRepository.Update(transactionForex);

            await PersistData(_transactionForexRepository.UnitOfWork);

            return new ConfirmCommandOutput
            {
                TransactionForexId = transactionForex.TransactionForexId,
            };


        }
        public async Task<List<NotificationMessage>> CheckValidations(PurchaseCommand request)
        {
            var lstNotifications = new List<NotificationMessage>();


            return lstNotifications;
        }
    }
}
