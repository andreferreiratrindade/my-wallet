using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using WalletService.Domain.DomainEvents;
using WalletService.Domain.Models.Entities.Ids;


namespace WalletService.Domain.Models.Entities
{
    public class Wallet : AggregateRoot, IAggregateRoot
    {

        public WalletId WalletId{get;set;}
        //public Guid Id {get {return WalletId.Value ; }}
        public string Name {get;set;}
        public string Symbol {get;set;}

        protected Wallet()
        {

        }

        protected override void When(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
