
using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using MongoDB.Driver.Core.Operations;
using ForexService.Domain.DomainEvents;
using ForexService.Domain.Models.Entities.Ids;


namespace ForexService.Domain.Models.Entities
{
    public class Forex : AggregateRoot, IAggregateRoot
    {

        public ForexId ForexId{get;set;}
        //public Guid Id {get {return ForexId.Value ; }}
        public string Name {get;set;}
        public string Symbol {get;set;}

        public static Forex Create(string name, string symbol){
            return new Forex(name, symbol);
        }

        protected Forex()
        {

        }

        private Forex(string name, string symbol){
            this.ForexId = new ForexId(Guid.NewGuid());
            this.Name = name;
            this.Symbol = symbol;
        }

        protected override void When(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
