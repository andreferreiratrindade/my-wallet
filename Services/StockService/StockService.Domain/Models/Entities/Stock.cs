using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using MongoDB.Driver.Core.Operations;
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

        public static Stock Create(string name, string symbol){
            return new Stock(name, symbol);
        }

        protected Stock()
        {

        }

        private Stock(string name, string symbol){
            this.StockId = new StockId(Guid.NewGuid());
            this.Name = name;
            this.Symbol = symbol;
        }

        protected override void When(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
