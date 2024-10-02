using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using WalletService.Domain.DomainEvents;
using WalletService.Domain.Models.Entities.Ids;

namespace WalletService.Application.Commands.Sell
{
    public class SellCommand : Command<SellCommandOutput>
    {


        [Required]
        public decimal Amount {get;set;}
        [Required]
        public decimal Value {get;set;}
        [Required]
        public string Symbol {get;set;}
        [Required]
        public DateTime InvestmentDate {get;set;}


        public SellCommand(  decimal amount,
                                decimal value,
                                string symbol,
                               DateTime InvestmentDate ):base(CorrelationId.Create())
        {
            this.Amount = amount;
            this.Value = value;
            this.InvestmentDate = InvestmentDate;
            this.Symbol = symbol.ToUpper();

            this.AddRollBackEvent(new TransactionSoldCompensationEvent(this.CorrelationId));
        }
    }

    public class SellCommandOutput : OutputCommand
    {

        public Guid TransactionId { get; internal set; }
        public decimal Amount { get; internal set; }
        public decimal Value { get; internal set; }
        public string Symbol { get; internal set; }
        public DateTime InvestmentDate { get; internal set; }
    }
}
