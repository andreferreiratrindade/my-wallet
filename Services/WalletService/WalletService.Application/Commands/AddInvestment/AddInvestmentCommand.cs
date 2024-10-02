using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using WalletService.Domain.DomainEvents;
using WalletService.Domain.Models.Entities.Ids;

namespace WalletService.Application.Commands.Purchase
{
    public class AddInvestmentCommand : Command<AddInvestmentCommandOutput>
    {
       [Required]
        public decimal Value {get;set;}

        [Required]
        public TypeInvestment TypeInvestment {get;set;}

        public AddInvestmentCommand(decimal value,
                                TypeInvestment typeInvestment):base(CorrelationId.Create())
        {
            this.TypeInvestment = typeInvestment;
            this.Value = value;

            this.AddRollBackEvent(new TransactionPurchasedCompensationEvent(this.CorrelationId));
        }
    }

    public class AddInvestmentCommandOutput : OutputCommand
    {


        public decimal Value {get;set;}


        public TypeInvestment TypeInvestment {get;set;}
    }
}
