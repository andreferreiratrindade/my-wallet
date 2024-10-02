using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using MassTransit.Futures.Contracts;
using StockService.Domain.DomainEvents;
using StockService.Domain.Models.Entities.Ids;


namespace StockService.Domain.Models.Entities
{
    public class StockResultTransaction : AggregateRoot, IAggregateRoot
    {
        public StockResultTransactionId  StockResultTransactionId {get;set;}
        public decimal TotalAmount {get;set;}
        public decimal TotalValue {get;set;}
        public StockId StockId {get;set;}

        public virtual Stock Stock {get;set;}
        protected StockResultTransaction()
        {

        }

        public static StockResultTransaction Create(
                                    StockId stockId,
                                    CorrelationId correlationId)
        {

            var stockResultTransaction = new StockResultTransaction(stockId, correlationId);

            return stockResultTransaction;
        }

        public void Add(decimal amount, decimal value, CorrelationId CorrelationId){
            var @event = new StockResultTransactionAddedEvent(this.StockResultTransactionId,
                                                    amount,
                                                    value,
                                                    this.StockId,
                                                    CorrelationId);
            this.RaiseEvent(@event);
        }

        private StockResultTransaction(
                StockId stockId,
                CorrelationId correlationId)
        {

            var @event = new StockResultTransactionCreatedEvent(new StockResultTransactionId(Guid.NewGuid()),
                                                    stockId,
                                                    correlationId );
            this.RaiseEvent(@event);
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case StockResultTransactionCreatedEvent x: OnStockResultTransactionCreatedEvent(x); break;
                case StockResultTransactionAddedEvent x: OnStockResultTransactionAddedEvent(x); break;
            }
        }

        private void OnStockResultTransactionCreatedEvent(StockResultTransactionCreatedEvent @event)
        {
            StockResultTransactionId = @event.StockResultTransactionId;
            StockId = @event.StockId;
        }

        private void OnStockResultTransactionAddedEvent(StockResultTransactionAddedEvent @event)
        {
           this.TotalAmount += @event.Amount;
           this.TotalValue += @event.Value * @event.Amount;
        }
    }
}
