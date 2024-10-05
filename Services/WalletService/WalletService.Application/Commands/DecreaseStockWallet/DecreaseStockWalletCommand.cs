using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Framework.Core.DomainObjects;
using WalletService.Domain.DomainEvents;

namespace WalletService.Application.Commands.Sell
{
    public class DecreaseStockWalletCommand : Command<DecreaseStockWalletCommandOutput>
    {

        [Required]
        public Guid TransactionStockId {get;set;}

        [Required]
        public decimal Amount {get;set;}
        [Required]
        public string Symbol {get;set;}


        public DecreaseStockWalletCommand(Guid transactionStockId,
                                          decimal amount,
                                          string symbol,
                                          CorrelationId correlactionId) :base(correlactionId)
        {
            this.Amount = amount;
            this.TransactionStockId = transactionStockId;

            this.Symbol = symbol.ToUpper();

            this.AddRollBackEvent(new StockWalletDecreasedCompensationEvent(this.TransactionStockId,this.CorrelationId));
        }
    }

    public class DecreaseStockWalletCommandOutput : OutputCommand
    {

        public Guid TransactionStockId { get; internal set; }
        public decimal Amount { get; internal set; }
        public decimal Value { get; internal set; }
        public string Symbol { get; internal set; }
        public DateTime InvestmentDate { get; internal set; }
    }
}
