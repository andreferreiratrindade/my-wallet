
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using StockService.Domain.Models.Entities.Ids;

namespace StockService.Domain.DomainEvents
{
    public class StockResultTransactionAddedEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public StockId StockId {get;set;}
        public StockResultTransactionId StockResultTransactionId { get; set; }

        public StockResultTransactionAddedEvent(StockResultTransactionId stockResultTransactionId,
                                        decimal amount,
                                         decimal value,
                                         StockId stockId,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.StockResultTransactionId = stockResultTransactionId;
            this.Amount = amount;
            this.Value = value;
            this.StockId = stockId;
        }
    }
}
