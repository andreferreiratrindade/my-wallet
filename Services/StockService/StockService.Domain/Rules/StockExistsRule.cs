using FluentValidation;
using Framework.Core.DomainObjects;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Repositories;
using System.Data;

namespace Activities.Domain.Rules
{
    public class StockExistsRule :IBusinessRule
    {
        private string Symbol;
        private List<string> MessageDetail = new();
        private readonly IStockRepository _stockRepository;


        public StockExistsRule( string symbol, IStockRepository stockRepository)
        {
            Symbol = symbol;
            _stockRepository =  stockRepository;
        }


        List<string> IBusinessRule.Message => MessageDetail;


        public async Task<bool> IsBroken()
        {
            var stock = await _stockRepository.GetBySymbol(Symbol);
            if(stock == null){
                MessageDetail.Add($"Stock '{Symbol}' not found");
            }

            return MessageDetail.Count != 0;
        }
    }
}
