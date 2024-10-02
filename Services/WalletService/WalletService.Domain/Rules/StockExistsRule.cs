using FluentValidation;
using Framework.Core.DomainObjects;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Repositories;
using System.Data;

namespace Activities.Domain.Rules
{
    public class WalletExistsRule :IBusinessRule
    {
        private string Symbol;
        private List<string> MessageDetail = new();
        private readonly IWalletRepository _walletRepository;


        public WalletExistsRule( string symbol, IWalletRepository walletRepository)
        {
            Symbol = symbol;
            _walletRepository =  walletRepository;
        }


        List<string> IBusinessRule.Message => MessageDetail;


        public async Task<bool> IsBroken()
        {
            var wallet = await _walletRepository.GetBySymbol(Symbol);
            if(wallet == null){
                MessageDetail.Add($"Wallet '{Symbol}' not found");
            }

            return MessageDetail.Count != 0;
        }
    }
}
