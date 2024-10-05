using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using StockService.Domain.DomainEvents;
using StockService.Domain.Models.Entities.Ids;


namespace StockService.Domain.Models.Entities
{
    public class Transaction : AggregateRoot, IAggregateRoot
    {
        public TransactionStockId TransactionStockId {get;set;}
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public StockId StockId {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}

        public virtual Stock Stock {get;set;}
        protected Transaction()
        {

        }

           public static Transaction Sell(decimal amount,
                                    decimal value,
                                    StockId stockId,
                                    DateTime investmentDate,
                                    CorrelationId correlationId)
        {

            var transaction = new Transaction(amount, value, stockId, investmentDate, correlationId);
            transaction.ExecuteSell(correlationId);
            return transaction;
        }

        public static Transaction Purchase(decimal amount,
                                    decimal value,
                                    StockId stockId,
                                    DateTime investmentDate,
                                    CorrelationId correlationId)
        {

            var transaction = new Transaction(amount, value, stockId, investmentDate, correlationId);
            transaction.ExecutePurchase(correlationId);
            return transaction;
        }

        protected void ExecutePurchase(CorrelationId CorrelationId){
            var @event = new TransactionPurchasedEvent(this.TransactionStockId,
                                                        this.Amount,
                                                       this.Value,
                                                       this.StockId,
                                                       this.InvestmentDate,
                                                       CorrelationId);
            this.RaiseEvent(@event);
        }

          protected void ExecuteSell(CorrelationId CorrelationId){
            var @event = new TransactionSoldEvent(this.TransactionStockId,
                                                        this.Amount,
                                                       this.Value,
                                                       this.StockId,
                                                       this.InvestmentDate,
                                                       CorrelationId);
            this.RaiseEvent(@event);
        }

        private Transaction( decimal amount,
                decimal value,
                StockId stockId,
                DateTime investmentDate,
                CorrelationId correlationId)
        {

            var @event = new TransactionCreatedEvent(new TransactionStockId(Guid.NewGuid()),
                                                    amount,
                                                    value,
                                                    stockId,
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
                case TransactionSoldEvent x: OnTransactionSoldEvent(x); break;

            }
        }

        private void OnTransactionCreatedEvent(TransactionCreatedEvent @event)
        {
            TransactionStockId = @event.TransactionStockId;
            Amount = @event.Amount;
            Value = @event.Value;
            StockId = @event.StockId;
            InvestmentDate = @event.InvestmentDate;
        }

        private void OnTransactionPurchasedEvent(TransactionPurchasedEvent @event)
        {
            this.TypeOperationInvestment = @event.TypeOperationInvestment;
        }

          private void OnTransactionSoldEvent(TransactionSoldEvent @event)
        {
            this.TypeOperationInvestment = @event.TypeOperationInvestment;
        }
    }
}
