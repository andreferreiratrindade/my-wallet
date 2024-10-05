using MediatR;
using MassTransit;
using Framework.MessageBus;
using StockService.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using StockService.Domain.Models.Repositories;

namespace StockService.Application.Events
{
    public class TransactionPurchaseRequestedEventHandler : INotificationHandler<TransactionPurchaseRequestedEvent>
    {
        private readonly IMessageBus _messageBus;
        private readonly IStockRepository _stockRepository;
        public TransactionPurchaseRequestedEventHandler(IMessageBus messageBus, IStockRepository stockRepository)
        {
            _messageBus = messageBus;
            _stockRepository =stockRepository;
        }

        public async Task Handle(TransactionPurchaseRequestedEvent message, CancellationToken cancellationToken)
        {
            var symbolStock = await _stockRepository.GetById(message.StockId);
            await _messageBus.PublishAsync(
                       new StockPurchasedIntegrationEvent(message.TransactionStockId.Value,
                                                     message.Amount ,
                                                      message.Value,
                                                      symbolStock.Symbol,
                                                      message.InvestmentDate,
                                                      message.TypeOperationInvestment,
                                                      message.CorrelationId),cancellationToken);
        }
    }
}
