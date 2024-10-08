using Framework.Core.Data;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Entities.Ids;

namespace ForexService.Domain.Models.Repositories
{
    public interface ITransactionForexRepository : IRepository<TransactionForex>
    {
        void Update(TransactionForex activity);
        void Add(TransactionForex activity);
        void Delete(Guid activityId);
        Task<TransactionForex?>  GetById(TransactionForexId transactionForexId);
        Task<TransactionForex?> GetByIdAsync(Guid activityId);

        IQueryable<TransactionForex> GetQueryable();
    }
}
