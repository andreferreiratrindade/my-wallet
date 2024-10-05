
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Entities.Ids;

namespace WalletService.Domain.DomainEvents
{
    public class StockWalletCreatedEvent : DomainEvent
    {
        public string Symbol {get;set;}
        public StockWalletId StockWalletId  { get; set; }

        public StockWalletCreatedEvent(StockWalletId stockWalletId,
                                        string symbol,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.StockWalletId = stockWalletId;
            this.Symbol = symbol;

        }
    }

     public class StockWalletCreatedCompensationEvent : RollBackEvent
    {
        public StockWalletCreatedCompensationEvent( CorrelationId correlationId):base(correlationId)
        {

        }
    }
}
