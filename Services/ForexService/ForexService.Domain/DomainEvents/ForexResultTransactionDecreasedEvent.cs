
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using ForexService.Domain.Models.Entities.Ids;

namespace ForexService.Domain.DomainEvents
{
    public class ForexResultTransactionDecreasedEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public ForexId ForexId {get;set;}
        public ForexResultTransactionId ForexResultTransactionId { get; set; }

        public ForexResultTransactionDecreasedEvent(ForexResultTransactionId forexResultTransactionId,
                                        decimal amount,
                                         decimal value,
                                         ForexId forexId,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.ForexResultTransactionId = forexResultTransactionId;
            this.Amount = amount;
            this.Value = value;
            this.ForexId = forexId;
        }
    }
}
