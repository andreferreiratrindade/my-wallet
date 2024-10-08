using Framework.Core.Data;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Entities.Ids;

namespace ForexService.Domain.Models.Repositories
{
    public interface IForexResultTransactionRepository : IRepository<ForexResultTransaction>
    {
        void Update(ForexResultTransaction forexResultTransaction);
        void Add(ForexResultTransaction forexResultTransaction);
        void Delete(Guid forexId);
        Task<ForexResultTransaction>  GetByForexId(ForexId forexId);


    }
}
