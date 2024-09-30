using Framework.Core.Data;
using StockService.Domain.Models.Entities;

namespace StockService.Domain.Models.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        void Update(Transaction activity);
        void Add(Transaction activity);
        void Delete(Guid activityId);
        Transaction? GetById(Guid activityId);
        Task<Transaction?> GetByIdAsync(Guid activityId);

        IQueryable<Transaction> GetQueryable();
    }
}
