
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using StockService.Domain.Enums;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Entities.Ids;

namespace StockService.Domain.DomainEvents
{
    public class TransactionPurchaseRequestedEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public StockId StockId {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}
        public StatusTransactionStock StatusTransactionStock { get; set; }
        public DateTime InvestmentDate {get;set;}
        public TransactionStockId TransactionStockId {get;set;}

        public TransactionPurchaseRequestedEvent(TransactionStockId transactionStockId,
                                        decimal amount,
                                         decimal value,
                                         StockId stockId,
                                         DateTime investmentDate,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.Amount = amount;
            this.Value = value;
            this.StockId = stockId;
            this.TransactionStockId = transactionStockId;
            this.InvestmentDate = investmentDate;
            this.TypeOperationInvestment = TypeOperationInvestment.Purchase;
            this.StatusTransactionStock = StatusTransactionStock.PENDING;
        }
    }

    public class TransactionPurchasedCompensationEvent : RollBackEvent
    {
        public TransactionPurchasedCompensationEvent( CorrelationId correlationId):base(correlationId)
        {

        }
    }
}
