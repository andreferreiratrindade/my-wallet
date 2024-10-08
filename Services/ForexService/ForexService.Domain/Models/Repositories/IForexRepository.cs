using Framework.Core.Data;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Entities.Ids;

namespace ForexService.Domain.Models.Repositories
{
    public interface IForexRepository : IRepository<Forex>
    {

        Task<Forex> GetById(ForexId ForexId);

        Task<Forex> GetBySymbol(string symbol);
    }
}
