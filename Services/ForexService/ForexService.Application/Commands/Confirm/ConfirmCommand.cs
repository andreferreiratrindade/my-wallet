using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Framework.Core.DomainObjects;
using ForexService.Domain.DomainEvents;
using ForexService.Domain.Models.Entities.Ids;

namespace ForexService.Application.Commands.Purchase
{
    public class ConfirmCommand : Command<ConfirmCommandOutput>
    {
        [Required]
        public TransactionForexId TransactionForexId {get;set;}

        public ConfirmCommand(  TransactionForexId transactionForexId,
                               CorrelationId correlationId ):base(correlationId)
        {
            this.TransactionForexId = transactionForexId;

            this.AddRollBackEvent(new TransactionForexConfirmedCompensationEvent(this.TransactionForexId, this.CorrelationId));
        }
    }

    public class ConfirmCommandOutput : OutputCommand
    {
        public TransactionForexId TransactionForexId { get;  set; }

    }
}
