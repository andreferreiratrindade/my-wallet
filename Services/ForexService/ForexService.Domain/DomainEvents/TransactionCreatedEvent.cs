
using Framework.Core.DomainObjects;
using ForexService.Domain.Enums;
using ForexService.Domain.Models.Entities.Ids;

namespace ForexService.Domain.DomainEvents
{
    public class TransactionCreatedEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public ForexId ForexId {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TransactionForexId TransactionForexId {get;set;}
        public StatusTransactionForex StatusTransactionForex { get; set; }


        public TransactionCreatedEvent( TransactionForexId transactionForexId,
                                        decimal amount,
                                         decimal value,
                                         ForexId forexId,
                                         DateTime investmentDate,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.TransactionForexId = transactionForexId;
            this.Amount = amount;
            this.Value = value;
            this.ForexId = forexId;
            this.StatusTransactionForex = StatusTransactionForex.CREATED;
            this.InvestmentDate =investmentDate;
        }
    }
}
