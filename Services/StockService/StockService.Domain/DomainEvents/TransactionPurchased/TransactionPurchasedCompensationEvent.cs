
using Framework.Core.DomainObjects;
using MassTransit;

namespace StockService.Domain.DomainEvents
{
    public class TransactionPurchasedCompensationEvent : RollBackEvent
    {
        public TransactionPurchasedCompensationEvent( CorrelationId correlationId):base(correlationId)
        {

        }
    }
}
