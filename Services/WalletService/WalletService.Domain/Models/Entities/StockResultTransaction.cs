using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using MassTransit.Futures.Contracts;
using WalletService.Domain.DomainEvents;
using WalletService.Domain.Models.Entities.Ids;


namespace WalletService.Domain.Models.Entities
{
    public class WalletResultTransaction : AggregateRoot, IAggregateRoot
    {
        public WalletResultTransactionId  WalletResultTransactionId {get;set;}
        public decimal TotalAmount {get;set;}
        public decimal TotalValue {get;set;}
        public WalletId WalletId {get;set;}

        public virtual Wallet Wallet {get;set;}
        protected WalletResultTransaction()
        {

        }

        public static WalletResultTransaction Create(
                                    WalletId walletId,
                                    CorrelationId correlationId)
        {

            var walletResultTransaction = new WalletResultTransaction(walletId, correlationId);

            return walletResultTransaction;
        }

        public void Add(decimal amount, decimal value, CorrelationId CorrelationId){
            var @event = new WalletResultTransactionAddedEvent(this.WalletResultTransactionId,
                                                    amount,
                                                    value,
                                                    this.WalletId,
                                                    CorrelationId);
            this.RaiseEvent(@event);
        }

        public void Decrease(decimal amount, decimal value, CorrelationId CorrelationId){
            var @event = new WalletResultTransactionDecreasedEvent(this.WalletResultTransactionId,
                                                    amount,
                                                    value,
                                                    this.WalletId,
                                                    CorrelationId);
            this.RaiseEvent(@event);
        }



        private WalletResultTransaction(
                WalletId walletId,
                CorrelationId correlationId)
        {

            var @event = new WalletResultTransactionCreatedEvent(new WalletResultTransactionId(Guid.NewGuid()),
                                                    walletId,
                                                    correlationId );
            this.RaiseEvent(@event);
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case WalletResultTransactionCreatedEvent x: OnWalletResultTransactionCreatedEvent(x); break;
                case WalletResultTransactionAddedEvent x: OnWalletResultTransactionAddedEvent(x); break;
                case WalletResultTransactionDecreasedEvent x: OnWalletResultTransactionDecreasedEvent(x); break;
            }
        }

        private void OnWalletResultTransactionCreatedEvent(WalletResultTransactionCreatedEvent @event)
        {
            WalletResultTransactionId = @event.WalletResultTransactionId;
            WalletId = @event.WalletId;
        }

        private void OnWalletResultTransactionAddedEvent(WalletResultTransactionAddedEvent @event)
        {
           this.TotalAmount += @event.Amount;
           this.TotalValue += @event.Value * @event.Amount;
        }

           private void OnWalletResultTransactionDecreasedEvent(WalletResultTransactionDecreasedEvent @event)
        {
           this.TotalAmount -= @event.Amount;
           this.TotalValue -= @event.Value * @event.Amount;
        }

    }
}
