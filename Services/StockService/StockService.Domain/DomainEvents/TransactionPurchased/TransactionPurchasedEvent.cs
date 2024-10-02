
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Entities.Ids;

namespace StockService.Domain.DomainEvents
{
    public class TransactionPurchasedEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public StockId StockId {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TransactionId TransactionId {get;set;}

        public TransactionPurchasedEvent(TransactionId transactionId,
                                        decimal amount,
                                         decimal value,
                                         StockId stockId,
                                         DateTime investmentDate,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.Amount = amount;
            this.Value = value;
            this.StockId = stockId;
            this.TransactionId = transactionId;
            this.InvestmentDate = investmentDate;
            this.TypeOperationInvestment = TypeOperationInvestment.Purchase;
        }
    }
}
