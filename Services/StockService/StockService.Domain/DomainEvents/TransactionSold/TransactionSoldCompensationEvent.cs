
using Framework.Core.DomainObjects;
using MassTransit;

namespace StockService.Domain.DomainEvents
{
    public class TransactionSoldCompensationEvent : RollBackEvent
    {
        public TransactionSoldCompensationEvent( CorrelationId correlationId):base(correlationId)
        {

        }
    }
}
