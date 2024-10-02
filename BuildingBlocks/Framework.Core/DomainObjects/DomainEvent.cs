using Framework.Core.EventSourcingUtils;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.DomainObjects
{
    public abstract class DomainEvent : EventSourcingNotification, IDomainEvent
    {
        public Guid EventId { get; private set; }
       // public AggregateId AggregateId { get;  }
        public long AggregateVersion { get; set; }
        public DateTime TimeStamp { get; }

        public CorrelationId CorrelationId { get; }

        private DomainEvent()
        {
            EventId = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
        }

        protected DomainEvent(CorrelationId correlationId):this(){
            this.CorrelationId = correlationId;
        }



        // protected DomainEvent(Guid aggregateId) : this()
        // {
        //     AggregateId = aggregateId;
        // }

        // protected DomainEvent(Guid aggregateId, long aggregateVersion) : this(aggregateId)
        // {
        //     AggregateVersion = aggregateVersion;
        // }
    }
}
