using Framework.Core.Data;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Entities.Ids;

namespace StockService.Domain.Models.Repositories
{
    public interface IStockRepository : IRepository<Stock>
    {

        Task<Stock> GetById(StockId StockId);

        Task<Stock> GetBySymbol(string symbol);
    }
}
