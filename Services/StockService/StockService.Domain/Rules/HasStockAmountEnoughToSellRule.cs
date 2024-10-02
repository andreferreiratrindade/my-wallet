using FluentValidation;
using Framework.Core.DomainObjects;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Repositories;
using System.Data;

namespace Activities.Domain.Rules
{
    public class HasStockAmountEnoughToSellRule :IBusinessRule
    {
        private string Symbol;
        private decimal Amount;
        private List<string> MessageDetail = new();
        private readonly IStockRepository _stockRepository;
        private readonly IStockResultTransactionRepository _stockResultTransactioRepository;


        public HasStockAmountEnoughToSellRule(
            string symbol,
            decimal amount,
            IStockRepository stockRepository,
            IStockResultTransactionRepository stockResultTransactioRepository)
        {
            Symbol = symbol;
            Amount = amount;
            _stockRepository =  stockRepository;
            _stockResultTransactioRepository = stockResultTransactioRepository;
        }


        List<string> IBusinessRule.Message => MessageDetail;


        public async Task<bool> IsBroken()
        {
            var stock = await _stockRepository.GetBySymbol(Symbol);
            if(stock != null){
                var stockResultTransaction = await _stockResultTransactioRepository.GetByStockId(stock.StockId);
                if(stockResultTransaction.TotalAmount - Amount < 0){
                    MessageDetail.Add($"Stock has just {stockResultTransaction.TotalAmount} quantity and you want to sell {Amount} quantity");
                }
            }

            return MessageDetail.Any();
        }

    }
}
