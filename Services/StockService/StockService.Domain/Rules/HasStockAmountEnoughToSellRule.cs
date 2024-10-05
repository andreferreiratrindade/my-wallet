using FluentValidation;
using Framework.Core.DomainObjects;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Repositories;
using System.Data;

namespace StockService.Domain.Rules
{
    public class HasStockAmountEnoughToSellRule :IBusinessRule
    {
        private string Symbol;
        private decimal Amount;
        private List<string> MessageDetail = new();
        private readonly IStockRepository _stockRepository;
        private readonly IStockResultTransactionStockRepository _stockResultTransactioRepository;


        public HasStockAmountEnoughToSellRule(
            string symbol,
            decimal amount,
            IStockRepository stockRepository,
            IStockResultTransactionStockRepository stockResultTransactioRepository)
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
                if(stockResultTransaction == null || stockResultTransaction.TotalAmount - Amount < 0){
                    var totalAmount = stockResultTransaction?.TotalAmount?? 0;
                    MessageDetail.Add($"Stock has just {totalAmount} quantity and you want to sell {Amount} quantity");
                }
            }

            return MessageDetail.Any();
        }

    }
}
