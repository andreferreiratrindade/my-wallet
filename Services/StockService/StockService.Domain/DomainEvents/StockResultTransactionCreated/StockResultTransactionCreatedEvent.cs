
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using StockService.Domain.Models.Entities.Ids;

namespace StockService.Domain.DomainEvents
{
    public class StockResultTransactionCreatedEvent : DomainEvent
    {

        public StockId StockId {get;set;}
        public StockResultTransactionStockId StockResultTransactionStockId {get;set;}

        public StockResultTransactionCreatedEvent( StockResultTransactionStockId stockResultTransactionStockId,
                                         StockId stockId,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.StockResultTransactionStockId = stockResultTransactionStockId;

            this.StockId = stockId;
        }
    }
}
