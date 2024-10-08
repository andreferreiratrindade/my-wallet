using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using ForexService.Domain.DomainEvents;
using ForexService.Domain.Models.Entities.Ids;

namespace ForexService.Application.Commands.Purchase
{
    public class PurchaseCommand : Command<PurchaseCommandOutput>
    {


        [Required]
        public decimal Amount {get;set;}
        [Required]
        public decimal Value {get;set;}
        [Required]
        public string Symbol {get;set;}
        [Required]
        public DateTime InvestmentDate {get;set;}


        public PurchaseCommand(  decimal amount,
                                decimal value,
                                string symbol,
                               DateTime InvestmentDate ):base(CorrelationId.Create())
        {
            this.Amount = amount;
            this.Value = value;
            this.InvestmentDate = InvestmentDate;
            this.Symbol = symbol.ToUpper();

            this.AddRollBackEvent(new TransactionPurchasedCompensationEvent(this.CorrelationId));
        }
    }

    public class PurchaseCommandOutput : OutputCommand
    {

        public Guid TransactionForexId { get; internal set; }
        public decimal Amount { get; internal set; }
        public decimal Value { get; internal set; }
        public string Symbol { get; internal set; }
        public DateTime InvestmentDate { get; internal set; }
    }
}
