using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using WalletService.Domain.DomainEvents;
using WalletService.Domain.Models.Entities.Ids;


namespace WalletService.Domain.Models.Entities
{
    public class StockWallet : AggregateRoot, IAggregateRoot
    {

        public StockWalletId StockWalletId{get;set;}
        public string Symbol {get;set;}
        public decimal Amount {get;set;}

        protected StockWallet()
        {

        }

        private StockWallet(string symbol, CorrelationId correlationId){
            var @event = new StockWalletCreatedEvent(new StockWalletId(Guid.NewGuid()), symbol,  correlationId);
            this.RaiseEvent(@event);
        }

        public static StockWallet Create(string symbol,CorrelationId correlationId){
            var stockWallet = new StockWallet(symbol,  correlationId);
            return stockWallet;
        }

        protected override void When(IDomainEvent @event)
        {
           switch (@event)
            {
                case StockWalletAddedEvent x: OnStockWalletAddedEvent(x); break;
                case StockWalletDecreasedEvent x: OnStockWalletDecreasedEvent(x); break;
                case StockWalletCreatedEvent x: OnStockWalletCreatedEvent(x); break;
            }
        }

        private void OnStockWalletCreatedEvent(StockWalletCreatedEvent @event)
        {

            Symbol = @event.Symbol;
            StockWalletId = @event.StockWalletId;
        }

        private void OnStockWalletAddedEvent(StockWalletAddedEvent @event)
        {
            Amount += @event.Amount;
        }


        private void OnStockWalletDecreasedEvent(StockWalletDecreasedEvent @event)
        {
            Amount += @event.Amount;
        }

        public void AddStock(Guid transactionStockId ,decimal amount, CorrelationId correlationId)
        {
           var @event = new StockWalletAddedEvent(this.StockWalletId,transactionStockId, amount,this.Symbol,  correlationId);
           this.RaiseEvent(@event);
        }

        public void DecreaseStock(Guid transactionStockId ,decimal amount, CorrelationId correlationId)
        {
            var @event = new StockWalletDecreasedEvent( this.StockWalletId,transactionStockId, amount,this.Symbol, correlationId);
           this.RaiseEvent(@event);
        }
    }
}
