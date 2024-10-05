using EasyNetQ;
using Framework.Core.DomainObjects;
using Framework.Core.Messages.Integration;
using Framework.Core.Notifications;
using Framework.Shared.IntegrationEvent.Enums;

namespace Framework.Shared.IntegrationEvent.Integration
{


    public class StockWalletAddedConfirmedIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
        public Guid StockWalletId  {get;set;}
        public Guid TransactionStokId  {get;set;}
        public string Symbol {get;set;}
        public decimal Amount {get;set;}
        public StockWalletAddedConfirmedIntegrationEvent(
                                    Guid transactionStokId,
                                    Guid stockWalletId,
                                    decimal amount,
                                    string symbol,
                                    CorrelationId correlationId):base(correlationId)
        {
            Amount = amount;
            TransactionStokId = transactionStokId;
            Symbol = symbol;
            StockWalletId = stockWalletId;

        }
    }
}
