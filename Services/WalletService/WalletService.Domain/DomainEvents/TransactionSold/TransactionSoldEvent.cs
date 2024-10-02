
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Entities.Ids;

namespace WalletService.Domain.DomainEvents
{
    public class TransactionSoldEvent : DomainEvent
    {
        public decimal Amount {get;set;}
        public decimal Value {get;set;}
        public WalletId WalletId {get;set;}
        public TypeOperationInvestment TypeOperationInvestment {get;set;}
        public DateTime InvestmentDate {get;set;}
        public TransactionId TransactionId {get;set;}

        public TransactionSoldEvent(TransactionId transactionId,
                                        decimal amount,
                                         decimal value,
                                         WalletId walletId,
                                         DateTime investmentDate,
                                         CorrelationId CorrelationId) :base(CorrelationId)
        {
            this.Amount = amount;
            this.Value = value;
            this.WalletId = walletId;
            this.TransactionId = transactionId;
            this.InvestmentDate = investmentDate;
            this.TypeOperationInvestment = TypeOperationInvestment.Sale;
        }
    }
}
