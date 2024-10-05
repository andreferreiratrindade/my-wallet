
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Entities.Ids;

namespace WalletService.Domain.DomainEvents
{
    public class StockWalletDecreasedEvent : DomainEvent
    {
        public Guid TransactionStockId {get;set;}

        public decimal Amount {get;set;}
        public string Symbol { get; set; }
        public StockWalletId StockWalletId  { get; set; }

        public StockWalletDecreasedEvent(StockWalletId stockWalletId,
                                    Guid transactionStockId,
                                    decimal amount,
                                    string symbol,
                                    CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.StockWalletId = stockWalletId;
            this.Amount = amount;
            this.Symbol = symbol;
            this.TransactionStockId = transactionStockId;
        }
    }

     public class StockWalletDecreasedCompensationEvent : RollBackEvent
    {
        public Guid TransactionStockId {get;set;}
        public StockWalletDecreasedCompensationEvent(Guid transactionStockId,  CorrelationId correlationId):base(correlationId)
        {
                TransactionStockId = transactionStockId;
        }
    }
}
