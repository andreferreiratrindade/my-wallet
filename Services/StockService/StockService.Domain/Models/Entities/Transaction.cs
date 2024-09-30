using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using StockService.Domain.DomainEvents;


namespace StockService.Domain.Models.Entities
{
    public class Transaction : AggregateRoot, IAggregateRoot
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public string Symbol {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}
        protected Transaction()
        {

        }

        public static Transaction Purchase(decimal amount,
                                    decimal value,
                                    string symbol,
                                    DateTime investmentDate,
                                    CorrelationIdGuid correlationId)
        {

            var transaction = new Transaction(amount, value, symbol, investmentDate, correlationId);
            transaction.ExecutePurchase(correlationId);
            return transaction;
        }

        protected void ExecutePurchase(CorrelationIdGuid correlationIdGuid){
            var @event = new TransactionPurchasedEvent(this.Amount,
                                                       this.Value,
                                                       this.Symbol,
                                                       this.InvestmentDate,
                                                       correlationIdGuid);
            this.RaiseEvent(@event);
        }

        private Transaction( decimal amount,
                decimal value,
                string symbol,
                DateTime investmentDate,
                CorrelationIdGuid correlationId)
        {

            var @event = new TransactionCreatedEvent(Guid.NewGuid(),
                                                    amount,
                                                    value,
                                                    symbol,
                                                    investmentDate,
                                                    correlationId );
            this.RaiseEvent(@event);
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case TransactionCreatedEvent x: OnTransactionCreatedEvent(x); break;
                case TransactionPurchasedEvent x: OnTransactionPurchasedEvent(x); break;

            }
        }

        private void OnTransactionCreatedEvent(TransactionCreatedEvent @event)
        {
            AggregateId = @event.AggregateId;
            Amount = @event.Amount;
            Value = @event.Value;
            Symbol = @event.Symbol;
            InvestmentDate = @event.InvestmentDate;
        }

        private void OnTransactionPurchasedEvent(TransactionPurchasedEvent @event)
        {
            this.TypeOperationInvestment = @event.TypeOperationInvestment;
        }
    }
}
