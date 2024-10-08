
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using ForexService.Domain.Models.Entities.Ids;

namespace ForexService.Domain.DomainEvents
{
    public class ForexResultTransactionCreatedEvent : DomainEvent
    {

        public ForexId ForexId {get;set;}
        public ForexResultTransactionId ForexResultTransactionId {get;set;}

        public ForexResultTransactionCreatedEvent( ForexResultTransactionId forexResultTransactionId,
                                         ForexId forexId,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.ForexResultTransactionId = forexResultTransactionId;

            this.ForexId = forexId;
        }
    }
}
