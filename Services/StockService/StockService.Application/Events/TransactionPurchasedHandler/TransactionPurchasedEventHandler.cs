using MediatR;
using MassTransit;
using Framework.MessageBus;
using StockService.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using StockService.Domain.Models.Repositories;

namespace StockService.Application.Events
{
    public class TransactionPurchasedEventHandler : INotificationHandler<TransactionPurchasedEvent>
    {
        private readonly IMessageBus _messageBus;
        private readonly IStockRepository _stockRepository;
        public TransactionPurchasedEventHandler(IMessageBus messageBus, IStockRepository stockRepository)
        {
            _messageBus = messageBus;
            _stockRepository =stockRepository;
        }

        public async Task Handle(TransactionPurchasedEvent message, CancellationToken cancellationToken)
        {
            var symbolStock = await _stockRepository.GetById(message.StockId);
            await _messageBus.PublishAsync(
                       new StockPurchasedIntegrationEvent(message.TransactionId.Value,
                                                     message.Amount ,
                                                      message.Value,
                                                      symbolStock.Symbol,
                                                      message.InvestmentDate,
                                                      message.TypeOperationInvestment,
                                                      message.CorrelationId),cancellationToken);
        }
    }
}
