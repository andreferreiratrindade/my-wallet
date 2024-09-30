
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;

namespace StockService.Domain.DomainEvents
{
    public class TransactionCreatedEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public string Symbol {get;set;}
        public DateTime InvestmentDate {get;set;}

        public TransactionCreatedEvent( Guid aggregationId,
                                        decimal amount,
                                         decimal value,
                                         string symbol,
                                         DateTime investmentDate,
                                         CorrelationIdGuid correlationIdGuid) :base(correlationIdGuid)
        {
            this.AggregateId = aggregationId;
            this.Amount = amount;
            this.Value = value;
            this.Symbol = symbol;

            this.InvestmentDate =investmentDate;
        }
    }
}
