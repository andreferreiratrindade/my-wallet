
using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using WalletService.Domain.DomainEvents;
using WalletService.Domain.Models.Entities.Ids;

namespace WalletService.Application.Commands.Purchase
{
    public class AddStockWalletCommand : Command<AddStockWalletCommandOutput>
    {
        [Required]
        public Guid TransactionStockId {get;set;}

        [Required]
        public decimal Amount {get;set;}
        [Required]
        public string Symbol {get;set;}

        public AddStockWalletCommand(Guid transactionStockId, decimal amount, string symbol, CorrelationId correlationId):base(CorrelationId.Create())
        {
            this.Amount = amount;
            this.Symbol = symbol;
            this.TransactionStockId = transactionStockId;
            this.AddRollBackEvent(new StockWalletAddedCompensationEvent(this.TransactionStockId,this.CorrelationId));
        }
    }

    public class AddStockWalletCommandOutput : OutputCommand
    {

        public Guid TransactionStockId {get;set;}
        public string Symbol {get;set;}
        public decimal Amount {get;set;}
    }
}
