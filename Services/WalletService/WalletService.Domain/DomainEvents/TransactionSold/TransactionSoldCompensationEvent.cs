
using Framework.Core.DomainObjects;
using MassTransit;

namespace WalletService.Domain.DomainEvents
{
    public class TransactionSoldCompensationEvent : RollBackEvent
    {
        public TransactionSoldCompensationEvent( CorrelationId correlationId):base(correlationId)
        {

        }
    }
}
