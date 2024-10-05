using EasyNetQ;
using Framework.Core.DomainObjects;
using Framework.Core.Messages.Integration;
using Framework.Core.Notifications;
using Framework.Shared.IntegrationEvent.Enums;

namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("StockPurchased", ExchangeName = "Stock")]

    public class StockPurchasedIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public string Symbol {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}
        public Guid TransactionStockId {get;set;}


        public StockPurchasedIntegrationEvent(
                                    Guid transactionStockId,
                                    decimal amount,
                                    decimal value,
                                    string symbol,
                                    DateTime investmentDate,
                                    TypeOperationInvestment typeOperationInvestment,
                                    CorrelationId correlationId):base(correlationId)
        {
            Amount = amount;
            Value = value;
            Symbol = symbol;
            InvestmentDate = investmentDate;
            TypeOperationInvestment = typeOperationInvestment;
            TransactionStockId = transactionStockId;
        }
    }
}
