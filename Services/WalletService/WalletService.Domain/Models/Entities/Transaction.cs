using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using WalletService.Domain.DomainEvents;
using WalletService.Domain.Models.Entities.Ids;


namespace WalletService.Domain.Models.Entities
{
    public class Transaction : AggregateRoot, IAggregateRoot
    {
        public TransactionId TransactionId {get;set;}
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public WalletId WalletId {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}

        public virtual Wallet Wallet {get;set;}
        protected Transaction()
        {

        }

           public static Transaction Sell(decimal amount,
                                    decimal value,
                                    WalletId walletId,
                                    DateTime investmentDate,
                                    CorrelationId correlationId)
        {

            var transaction = new Transaction(amount, value, walletId, investmentDate, correlationId);
            transaction.ExecuteSell(correlationId);
            return transaction;
        }

        public static Transaction Purchase(decimal amount,
                                    decimal value,
                                    WalletId walletId,
                                    DateTime investmentDate,
                                    CorrelationId correlationId)
        {

            var transaction = new Transaction(amount, value, walletId, investmentDate, correlationId);
            transaction.ExecutePurchase(correlationId);
            return transaction;
        }

        protected void ExecutePurchase(CorrelationId CorrelationId){
            var @event = new TransactionPurchasedEvent(this.TransactionId,
                                                        this.Amount,
                                                       this.Value,
                                                       this.WalletId,
                                                       this.InvestmentDate,
                                                       CorrelationId);
            this.RaiseEvent(@event);
        }

          protected void ExecuteSell(CorrelationId CorrelationId){
            var @event = new TransactionSoldEvent(this.TransactionId,
                                                        this.Amount,
                                                       this.Value,
                                                       this.WalletId,
                                                       this.InvestmentDate,
                                                       CorrelationId);
            this.RaiseEvent(@event);
        }

        private Transaction( decimal amount,
                decimal value,
                WalletId walletId,
                DateTime investmentDate,
                CorrelationId correlationId)
        {

            var @event = new TransactionCreatedEvent(new TransactionId(Guid.NewGuid()),
                                                    amount,
                                                    value,
                                                    walletId,
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
            TransactionId = @event.TransactionId;
            Amount = @event.Amount;
            Value = @event.Value;
            WalletId = @event.WalletId;
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
