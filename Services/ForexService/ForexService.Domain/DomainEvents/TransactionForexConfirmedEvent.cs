
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using ForexService.Domain.Enums;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Entities.Ids;

namespace ForexService.Domain.DomainEvents
{
    public class TransactionForexConfirmedEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public ForexId ForexId {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TransactionForexId TransactionForexId {get;set;}
        public StatusTransactionForex StatusTransactionForex { get; set; }


        public TransactionForexConfirmedEvent(TransactionForexId transactionForexId,
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
            this.TypeOperationInvestment = TypeOperationInvestment.Sale;
            this.StatusTransactionForex = StatusTransactionForex.CONFIRMED;
        }
    }
         public class TransactionForexConfirmedCompensationEvent : RollBackEvent
    {
        public TransactionForexConfirmedCompensationEvent(TransactionForexId TransactionForexId   ,  CorrelationId correlationId):base(correlationId)
        {

        }
    }
}
