using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using MassTransit.Futures.Contracts;
using ForexService.Domain.DomainEvents;
using ForexService.Domain.Models.Entities.Ids;


namespace ForexService.Domain.Models.Entities
{
    public class ForexResultTransaction : AggregateRoot, IAggregateRoot
    {
        public ForexResultTransactionId  ForexResultTransactionId {get;set;}
        public decimal TotalAmount {get;set;}
        public decimal TotalValue {get;set;}
        public ForexId ForexId {get;set;}

        public virtual Forex Forex {get;set;}
        protected ForexResultTransaction()
        {

        }

        public static ForexResultTransaction Create(
                                    ForexId forexId,
                                    CorrelationId correlationId)
        {

            var forexResultTransaction = new ForexResultTransaction(forexId, correlationId);

            return forexResultTransaction;
        }

        public void Add(decimal amount, decimal value, CorrelationId CorrelationId){
            var @event = new ForexResultTransactionAddedEvent(this.ForexResultTransactionId,
                                                    amount,
                                                    value,
                                                    this.ForexId,
                                                    CorrelationId);
            this.RaiseEvent(@event);
        }

        public void Decrease(decimal amount, decimal value, CorrelationId CorrelationId){
            var @event = new ForexResultTransactionDecreasedEvent(this.ForexResultTransactionId,
                                                    amount,
                                                    value,
                                                    this.ForexId,
                                                    CorrelationId);
            this.RaiseEvent(@event);
        }



        private ForexResultTransaction(
                ForexId forexId,
                CorrelationId correlationId)
        {

            var @event = new ForexResultTransactionCreatedEvent(new ForexResultTransactionId(Guid.NewGuid()),
                                                    forexId,
                                                    correlationId );
            this.RaiseEvent(@event);
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case ForexResultTransactionCreatedEvent x: OnForexResultTransactionCreatedEvent(x); break;
                case ForexResultTransactionAddedEvent x: OnForexResultTransactionAddedEvent(x); break;
                case ForexResultTransactionDecreasedEvent x: OnForexResultTransactionDecreasedEvent(x); break;
            }
        }

        private void OnForexResultTransactionCreatedEvent(ForexResultTransactionCreatedEvent @event)
        {
            ForexResultTransactionId = @event.ForexResultTransactionId;
            ForexId = @event.ForexId;
        }

        private void OnForexResultTransactionAddedEvent(ForexResultTransactionAddedEvent @event)
        {
           this.TotalAmount += @event.Amount;
           this.TotalValue += @event.Value * @event.Amount;
        }

           private void OnForexResultTransactionDecreasedEvent(ForexResultTransactionDecreasedEvent @event)
        {
           this.TotalAmount -= @event.Amount;
           this.TotalValue -= @event.Value * @event.Amount;
        }

    }
}
