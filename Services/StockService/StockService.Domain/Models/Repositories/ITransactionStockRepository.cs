using Framework.Core.Data;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Entities.Ids;

namespace StockService.Domain.Models.Repositories
{
    public interface ITransactionStockRepository : IRepository<TransactionStock>
    {
        void Update(TransactionStock activity);
        void Add(TransactionStock activity);
        void Delete(Guid activityId);
        Task<TransactionStock?>  GetById(TransactionStockId transactionStockId);
        Task<TransactionStock?> GetByIdAsync(Guid activityId);

        IQueryable<TransactionStock> GetQueryable();
    }
}
