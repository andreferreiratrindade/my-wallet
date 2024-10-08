using FluentValidation;
using Framework.Core.DomainObjects;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Repositories;
using System.Data;

namespace ForexService.Domain.Rules
{
    public class ForexExistsRule :IBusinessRule
    {
        private string Symbol;
        private List<string> MessageDetail = new();
        private readonly IForexRepository _forexRepository;


        public ForexExistsRule( string symbol, IForexRepository forexRepository)
        {
            Symbol = symbol;
            _forexRepository =  forexRepository;
        }


        List<string> IBusinessRule.Message => MessageDetail;


        public async Task<bool> IsBroken()
        {
            var forex = await _forexRepository.GetBySymbol(Symbol);
            if(forex == null){
                MessageDetail.Add($"Forex '{Symbol}' not found");
            }

            return MessageDetail.Count != 0;
        }
    }
}
