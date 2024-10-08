using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using ForexService.Domain.DomainEvents;
using ForexService.Domain.Enums;
using ForexService.Domain.Models.Entities.Ids;


namespace ForexService.Domain.Models.Entities
{
    public class TransactionForex : AggregateRoot, IAggregateRoot
    {
        public TransactionForexId TransactionForexId {get;set;}
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public ForexId ForexId {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}
        public StatusTransactionForex StatusTransactionForex {get;set;}

        public virtual Forex Forex {get;set;}
        protected TransactionForex()
        {

        }

           public static TransactionForex Sell(decimal amount,
                                    decimal value,
                                    ForexId forexId,
                                    DateTime investmentDate,
                                    CorrelationId correlationId)
        {

            var transaction = new TransactionForex(amount, value, forexId, investmentDate, correlationId);
            transaction.ExecuteSell(correlationId);
            return transaction;
        }

        public static TransactionForex Purchase(decimal amount,
                                    decimal value,
                                    ForexId forexId,
                                    DateTime investmentDate,
                                    CorrelationId correlationId)
        {

            var transaction = new TransactionForex(amount, value, forexId, investmentDate, correlationId);
            transaction.ExecutePurchase(correlationId);
            return transaction;
        }

        protected void ExecutePurchase(CorrelationId CorrelationId){
            var @event = new TransactionPurchaseRequestedEvent(this.TransactionForexId,
                                                        this.Amount,
                                                       this.Value,
                                                       this.ForexId,
                                                       this.InvestmentDate,
                                                       CorrelationId);
            this.RaiseEvent(@event);
        }

          protected void ExecuteSell(CorrelationId CorrelationId){
            var @event = new TransactionSoldRequestedEvent(this.TransactionForexId,
                                                        this.Amount,
                                                       this.Value,
                                                       this.ForexId,
                                                       this.InvestmentDate,
                                                       CorrelationId);
            this.RaiseEvent(@event);
        }

        private TransactionForex( decimal amount,
                decimal value,
                ForexId forexId,
                DateTime investmentDate,
                CorrelationId correlationId)
        {

            var @event = new TransactionCreatedEvent(new TransactionForexId(Guid.NewGuid()),
                                                    amount,
                                                    value,
                                                    forexId,
                                                    investmentDate,
                                                    correlationId );
            this.RaiseEvent(@event);
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case TransactionCreatedEvent x: OnTransactionCreatedEvent(x); break;
                case TransactionPurchaseRequestedEvent x: OnTransactionPurchaseRequestedEvent(x); break;
                case TransactionSoldRequestedEvent x: OnTransactionSoldRequestedEvent(x); break;
                case TransactionForexConfirmedEvent x: OnTransactionForexConfirmedEvent(x); break;
            }
        }

        private void OnTransactionCreatedEvent(TransactionCreatedEvent @event)
        {
            TransactionForexId = @event.TransactionForexId;
            Amount = @event.Amount;
            Value = @event.Value;
            ForexId = @event.ForexId;
            InvestmentDate = @event.InvestmentDate;
            StatusTransactionForex = @event.StatusTransactionForex;
        }

        private void OnTransactionPurchaseRequestedEvent(TransactionPurchaseRequestedEvent @event)
        {
            this.TypeOperationInvestment = @event.TypeOperationInvestment;
            this.StatusTransactionForex = @event.StatusTransactionForex;
        }

          private void OnTransactionSoldRequestedEvent(TransactionSoldRequestedEvent @event)
        {
            this.TypeOperationInvestment = @event.TypeOperationInvestment;
            this.StatusTransactionForex = @event.StatusTransactionForex;

        }

          private void OnTransactionForexConfirmedEvent(TransactionForexConfirmedEvent @event)
        {
            this.StatusTransactionForex = @event.StatusTransactionForex;
        }

        public void Confirm(CorrelationId correlationId)
        {
            var @event = new TransactionForexConfirmedEvent(this.TransactionForexId,
                                                            this.Amount,
                                                            this.Value,
                                                            this.ForexId,
                                                            this.InvestmentDate,
                                                            correlationId);
            this.RaiseEvent(@event);
        }
    }
}
