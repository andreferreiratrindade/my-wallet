using FluentValidation;
using Framework.Core.DomainObjects;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Repositories;
using System.Data;

namespace Activities.Domain.Rules
{
    public class HasWalletAmountEnoughToSellRule :IBusinessRule
    {
        private string Symbol;
        private decimal Amount;
        private List<string> MessageDetail = new();
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletResultTransactionRepository _walletResultTransactioRepository;


        public HasWalletAmountEnoughToSellRule(
            string symbol,
            decimal amount,
            IWalletRepository walletRepository,
            IWalletResultTransactionRepository walletResultTransactioRepository)
        {
            Symbol = symbol;
            Amount = amount;
            _walletRepository =  walletRepository;
            _walletResultTransactioRepository = walletResultTransactioRepository;
        }


        List<string> IBusinessRule.Message => MessageDetail;


        public async Task<bool> IsBroken()
        {
            var wallet = await _walletRepository.GetBySymbol(Symbol);
            if(wallet != null){
                var walletResultTransaction = await _walletResultTransactioRepository.GetByWalletId(wallet.WalletId);
                if(walletResultTransaction.TotalAmount - Amount < 0){
                    MessageDetail.Add($"Wallet has just {walletResultTransaction.TotalAmount} quantity and you want to sell {Amount} quantity");
                }
            }

            return MessageDetail.Any();
        }

    }
}
