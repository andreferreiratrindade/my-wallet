
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
        public StockResultTransactionStockId StockResultTransactionStockId { get; set; }

        public StockResultTransactionAddedEvent(StockResultTransactionStockId stockResultTransactionStockId,
                                        decimal amount,
                                         decimal value,
                                         StockId stockId,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.StockResultTransactionStockId = stockResultTransactionStockId;
            this.Amount = amount;
            this.Value = value;
            this.StockId = stockId;
        }
    }
}
