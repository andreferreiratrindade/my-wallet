using MediatR;
using MassTransit;
using Framework.MessageBus;
using ForexService.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using ForexService.Domain.Models.Repositories;

namespace ForexService.Application.Events
{
    public class TransactionSoldRequestedEventHandler : INotificationHandler<TransactionSoldRequestedEvent>
    {
        private readonly IMessageBus _messageBus;
        private readonly IForexRepository _forexRepository;
        public TransactionSoldRequestedEventHandler(IMessageBus messageBus, IForexRepository forexRepository)
        {
            _messageBus = messageBus;
            _forexRepository =forexRepository;
        }

        public async Task Handle(TransactionSoldRequestedEvent message, CancellationToken cancellationToken)
        {
            var symbolForex = await _forexRepository.GetById(message.ForexId);
            // await _messageBus.PublishAsync(
            //            new ForexSoldIntegrationEvent(message.TransactionForexId.Value,
            //                                          message.Amount ,
            //                                           message.Value,
            //                                           symbolForex.Symbol,
            //                                           message.InvestmentDate,
            //                                           message.TypeOperationInvestment,
            //                                           message.CorrelationId),cancellationToken);
        }
    }
}
