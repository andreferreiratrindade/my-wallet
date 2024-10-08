
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using ForexService.Domain.Enums;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Entities.Ids;

namespace ForexService.Domain.DomainEvents
{
    public class TransactionPurchaseRequestedEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public ForexId ForexId {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}
        public StatusTransactionForex StatusTransactionForex { get; set; }
        public DateTime InvestmentDate {get;set;}
        public TransactionForexId TransactionForexId {get;set;}

        public TransactionPurchaseRequestedEvent(TransactionForexId transactionForexId,
                                        decimal amount,
                                         decimal value,
                                         ForexId forexId,
                                         DateTime investmentDate,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.Amount = amount;
            this.Value = value;
            this.ForexId = forexId;
            this.TransactionForexId = transactionForexId;
            this.InvestmentDate = investmentDate;
            this.TypeOperationInvestment = TypeOperationInvestment.Purchase;
            this.StatusTransactionForex = StatusTransactionForex.PENDING;
        }
    }

    public class TransactionPurchasedCompensationEvent : RollBackEvent
    {
        public TransactionPurchasedCompensationEvent( CorrelationId correlationId):base(correlationId)
        {

        }
    }
}
