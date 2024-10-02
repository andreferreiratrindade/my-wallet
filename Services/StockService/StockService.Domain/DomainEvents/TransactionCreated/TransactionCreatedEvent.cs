
using Framework.Core.DomainObjects;
using StockService.Domain.Models.Entities.Ids;

namespace StockService.Domain.DomainEvents
{
    public class TransactionCreatedEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public StockId StockId {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TransactionId TransactionId {get;set;}

        public TransactionCreatedEvent( TransactionId transactionId,
                                        decimal amount,
                                         decimal value,
                                         StockId stockId,
                                         DateTime investmentDate,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.TransactionId = transactionId;
            this.Amount = amount;
            this.Value = value;
            this.StockId = stockId;

            this.InvestmentDate =investmentDate;
        }
    }
}
