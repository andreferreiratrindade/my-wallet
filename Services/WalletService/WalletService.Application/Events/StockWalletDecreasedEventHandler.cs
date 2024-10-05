using MediatR;
using MassTransit;
using Framework.MessageBus;
using WalletService.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using WalletService.Domain.Models.Repositories;

namespace WalletService.Application.Events
{
    public class StockWalletDecreasedEventHandler :
    INotificationHandler<StockWalletDecreasedEvent>
    {
        private readonly IMessageBus _messageBus;
        private readonly IStockWalletRepository _stockWalletRepository;
        public StockWalletDecreasedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(StockWalletDecreasedEvent message, CancellationToken cancellationToken)
        {
            await _messageBus.PublishAsync(
                       new StockWalletDecreasedConfirmedIntegrationEvent(message.StockWalletId.Value,
                                                    message.TransactionStockId,
                                                     message.Amount ,
                                                     message.Symbol,
                                                      message.CorrelationId),cancellationToken);
        }


    }
}
