
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;

namespace StockService.Domain.DomainEvents
{
    public class TransactionPurchasedEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public string Symbol {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}
        public DateTime InvestmentDate {get;set;}




        public TransactionPurchasedEvent(decimal amount,
                                         decimal value,
                                         string symbol,
                                         DateTime investmentDate,
                                         CorrelationIdGuid correlationIdGuid) :base(correlationIdGuid)
        {
            this.Amount = amount;
            this.Value = value;
            this.Symbol = symbol;
            this.InvestmentDate = investmentDate;
            this.TypeOperationInvestment = TypeOperationInvestment.Purchase;
        }
    }
}
