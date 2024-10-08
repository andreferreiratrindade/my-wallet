using MediatR;
using MassTransit;
using Framework.MessageBus;
using ForexService.Domain.DomainEvents;
using ForexService.Domain.Models.Repositories;


namespace ForexService.Application.Events
{
    public class TransactionPurchaseRequestedEventHandler : INotificationHandler<TransactionPurchaseRequestedEvent>
    {
        private readonly IMessageBus _messageBus;
        private readonly IForexRepository _forexRepository;
        public TransactionPurchaseRequestedEventHandler(IMessageBus messageBus, IForexRepository forexRepository)
        {
            _messageBus = messageBus;
            _forexRepository =forexRepository;
        }

        public async Task Handle(TransactionPurchaseRequestedEvent message, CancellationToken cancellationToken)
        {
            var symbolForex = await _forexRepository.GetById(message.ForexId);
            // await _messageBus.PublishAsync(
            //            new ForexPurchasedIntegrationEvent(message.TransactionForexId.Value,
            //                                          message.Amount ,
            //                                           message.Value,
            //                                           symbolForex.Symbol,
            //                                           message.InvestmentDate,
            //                                           message.TypeOperationInvestment,
            //                                           message.CorrelationId),cancellationToken);
        }
    }
}
