using FluentValidation;
using Framework.Core.DomainObjects;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Repositories;
using System.Data;

namespace ForexService.Domain.Rules
{
    public class HasForexAmountEnoughToSellRule :IBusinessRule
    {
        private string Symbol;
        private decimal Amount;
        private List<string> MessageDetail = new();
        private readonly IForexRepository _forexRepository;
        private readonly IForexResultTransactionRepository _forexResultTransactioRepository;


        public HasForexAmountEnoughToSellRule(
            string symbol,
            decimal amount,
            IForexRepository forexRepository,
            IForexResultTransactionRepository forexResultTransactioRepository)
        {
            Symbol = symbol;
            Amount = amount;
            _forexRepository =  forexRepository;
            _forexResultTransactioRepository = forexResultTransactioRepository;
        }


        List<string> IBusinessRule.Message => MessageDetail;


        public async Task<bool> IsBroken()
        {
            var forex = await _forexRepository.GetBySymbol(Symbol);
            if(forex != null){
                var forexResultTransaction = await _forexResultTransactioRepository.GetByForexId(forex.ForexId);
                if(forexResultTransaction == null || forexResultTransaction.TotalAmount - Amount < 0){
                    var totalAmount = forexResultTransaction?.TotalAmount?? 0;
                    MessageDetail.Add($"Forex has just {totalAmount} quantity and you want to sell {Amount} quantity");
                }
            }

            return MessageDetail.Any();
        }

    }
}
