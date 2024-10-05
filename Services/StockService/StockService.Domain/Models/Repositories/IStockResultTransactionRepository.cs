using Framework.Core.Data;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Entities.Ids;

namespace StockService.Domain.Models.Repositories
{
    public interface IStockResultTransactionStockRepository : IRepository<StockResultTransaction>
    {
        void Update(StockResultTransaction stockResultTransaction);
        void Add(StockResultTransaction stockResultTransaction);
        void Delete(Guid stockId);
        Task<StockResultTransaction>  GetByStockId(StockId stockId);


    }
}
