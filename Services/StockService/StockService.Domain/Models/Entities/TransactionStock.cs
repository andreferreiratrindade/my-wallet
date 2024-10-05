using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using StockService.Domain.DomainEvents;
using StockService.Domain.Enums;
using StockService.Domain.Models.Entities.Ids;


namespace StockService.Domain.Models.Entities
{
    public class TransactionStock : AggregateRoot, IAggregateRoot
    {
        public TransactionStockId TransactionStockId {get;set;}
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public StockId StockId {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}
        public StatusTransactionStock StatusTransactionStock {get;set;}

        public virtual Stock Stock {get;set;}
        protected TransactionStock()
        {

        }

           public static TransactionStock Sell(decimal amount,
                                    decimal value,
                                    StockId stockId,
                                    DateTime investmentDate,
                                    CorrelationId correlationId)
        {

            var transaction = new TransactionStock(amount, value, stockId, investmentDate, correlationId);
            transaction.ExecuteSell(correlationId);
            return transaction;
        }

        public static TransactionStock Purchase(decimal amount,
                                    decimal value,
                                    StockId stockId,
                                    DateTime investmentDate,
                                    CorrelationId correlationId)
        {

            var transaction = new TransactionStock(amount, value, stockId, investmentDate, correlationId);
            transaction.ExecutePurchase(correlationId);
            return transaction;
        }

        protected void ExecutePurchase(CorrelationId CorrelationId){
            var @event = new TransactionPurchaseRequestedEvent(this.TransactionStockId,
                                                        this.Amount,
                                                       this.Value,
                                                       this.StockId,
                                                       this.InvestmentDate,
                                                       CorrelationId);
            this.RaiseEvent(@event);
        }

          protected void ExecuteSell(CorrelationId CorrelationId){
            var @event = new TransactionSoldRequestedEvent(this.TransactionStockId,
                                                        this.Amount,
                                                       this.Value,
                                                       this.StockId,
                                                       this.InvestmentDate,
                                                       CorrelationId);
            this.RaiseEvent(@event);
        }

        private TransactionStock( decimal amount,
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
                case TransactionPurchaseRequestedEvent x: OnTransactionPurchaseRequestedEvent(x); break;
                case TransactionSoldRequestedEvent x: OnTransactionSoldRequestedEvent(x); break;
                case TransactionStockConfirmedEvent x: OnTransactionStockConfirmedEvent(x); break;
            }
        }

        private void OnTransactionCreatedEvent(TransactionCreatedEvent @event)
        {
            TransactionStockId = @event.TransactionStockId;
            Amount = @event.Amount;
            Value = @event.Value;
            StockId = @event.StockId;
            InvestmentDate = @event.InvestmentDate;
            StatusTransactionStock = @event.StatusTransactionStock;
        }

        private void OnTransactionPurchaseRequestedEvent(TransactionPurchaseRequestedEvent @event)
        {
            this.TypeOperationInvestment = @event.TypeOperationInvestment;
            this.StatusTransactionStock = @event.StatusTransactionStock;
        }

          private void OnTransactionSoldRequestedEvent(TransactionSoldRequestedEvent @event)
        {
            this.TypeOperationInvestment = @event.TypeOperationInvestment;
            this.StatusTransactionStock = @event.StatusTransactionStock;

        }

          private void OnTransactionStockConfirmedEvent(TransactionStockConfirmedEvent @event)
        {
            this.StatusTransactionStock = @event.StatusTransactionStock;
        }

        public void Confirm(CorrelationId correlationId)
        {
            var @event = new TransactionStockConfirmedEvent(this.TransactionStockId,
                                                            this.Amount,
                                                            this.Value,
                                                            this.StockId,
                                                            this.InvestmentDate,
                                                            correlationId);
            this.RaiseEvent(@event);
        }
    }
}
