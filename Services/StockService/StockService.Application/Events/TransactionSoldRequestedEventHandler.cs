using MediatR;
using MassTransit;
using Framework.MessageBus;
using StockService.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using StockService.Domain.Models.Repositories;

namespace StockService.Application.Events
{
    public class TransactionSoldRequestedEventHandler : INotificationHandler<TransactionSoldRequestedEvent>
    {
        private readonly IMessageBus _messageBus;
        private readonly IStockRepository _stockRepository;
        public TransactionSoldRequestedEventHandler(IMessageBus messageBus, IStockRepository stockRepository)
        {
            _messageBus = messageBus;
            _stockRepository =stockRepository;
        }

        public async Task Handle(TransactionSoldRequestedEvent message, CancellationToken cancellationToken)
        {
            var symbolStock = await _stockRepository.GetById(message.StockId);
            await _messageBus.PublishAsync(
                       new StockSoldIntegrationEvent(message.TransactionStockId.Value,
                                                     message.Amount ,
                                                      message.Value,
                                                      symbolStock.Symbol,
                                                      message.InvestmentDate,
                                                      message.TypeOperationInvestment,
                                                      message.CorrelationId),cancellationToken);
        }
    }
}
