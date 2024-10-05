
using Framework.Core.DomainObjects;
using StockService.Domain.Enums;
using StockService.Domain.Models.Entities.Ids;

namespace StockService.Domain.DomainEvents
{
    public class TransactionCreatedEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public StockId StockId {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TransactionStockId TransactionStockId {get;set;}
        public StatusTransactionStock StatusTransactionStock { get; set; }


        public TransactionCreatedEvent( TransactionStockId transactionStockId,
                                        decimal amount,
                                         decimal value,
                                         StockId stockId,
                                         DateTime investmentDate,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.TransactionStockId = transactionStockId;
            this.Amount = amount;
            this.Value = value;
            this.StockId = stockId;
            this.StatusTransactionStock = StatusTransactionStock.CREATED;
            this.InvestmentDate =investmentDate;
        }
    }
}
