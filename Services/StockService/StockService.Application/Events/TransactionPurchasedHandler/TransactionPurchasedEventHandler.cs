using MediatR;
using MassTransit;
using Framework.MessageBus;
using StockService.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;

namespace StockService.Application.Events
{
    public class TransactionPurchasedEventHandler : INotificationHandler<TransactionPurchasedEvent>
    {
        private readonly IMessageBus _messageBus;
        public TransactionPurchasedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(TransactionPurchasedEvent message, CancellationToken cancellationToken)
        {
            await _messageBus.PublishAsync(
                       new StockPurchasedIntegrationEvent(message.AggregateId,
                                                     message.Amount ,
                                                      message.Value,
                                                      message.Symbol,
                                                      message.InvestmentDate,
                                                      message.TypeOperationInvestment,
                                                      message.CorrelationId),cancellationToken);
        }
    }
}
