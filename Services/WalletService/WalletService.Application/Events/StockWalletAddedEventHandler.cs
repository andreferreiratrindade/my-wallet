using MediatR;
using MassTransit;
using Framework.MessageBus;
using WalletService.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using WalletService.Domain.Models.Repositories;

namespace WalletService.Application.Events
{
    public class StockWalletAddedEventHandler :
    INotificationHandler<StockWalletAddedEvent>
    {
        private readonly IMessageBus _messageBus;
        public StockWalletAddedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(StockWalletAddedEvent message, CancellationToken cancellationToken)
        {
            await _messageBus.PublishAsync(
                       new StockWalletAddedConfirmedIntegrationEvent(message.StockWalletId.Value,
                                                    message.TransactionStockId,
                                                     message.Amount ,
                                                     message.Symbol,
                                                      message.CorrelationId),cancellationToken);
        }


    }
}
