using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using StockService.Domain.DomainEvents;
using StockService.Domain.Models.Entities.Ids;


namespace StockService.Domain.Models.Entities
{
    public class Stock : AggregateRoot, IAggregateRoot
    {

        public StockId StockId{get;set;}
        //public Guid Id {get {return StockId.Value ; }}
        public string Name {get;set;}
        public string Symbol {get;set;}

        protected Stock()
        {

        }

        protected override void When(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
