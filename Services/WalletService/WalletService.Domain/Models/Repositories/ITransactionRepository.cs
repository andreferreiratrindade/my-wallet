using Framework.Core.Data;
using WalletService.Domain.Models.Entities;

namespace WalletService.Domain.Models.Repositories
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
