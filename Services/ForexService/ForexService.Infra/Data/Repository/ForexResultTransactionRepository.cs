using System.Data.Common;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Entities.Ids;
using ForexService.Domain.Models.Repositories;

namespace ForexService.Infra.Data.Repository
{
    public class ForexResultTransactionRepository : IForexResultTransactionRepository
    {
        private readonly ForexContext _context;

        public ForexResultTransactionRepository(ForexContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(ForexResultTransaction entity)
        {
            _context.ForexResultTransactions.Add(entity);
        }

        public void Update(ForexResultTransaction entity)
        {
            _context.ForexResultTransactions.Update(entity);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public DbConnection GetConnection() => _context.Database.GetDbConnection();

        public void Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public ForexResultTransactionRepository? GetById(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ForexResultTransactionRepository> GetQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<ForexResultTransaction> GetBySymbolAsync(string Symbol)
        {
            throw new NotImplementedException();
        }



        public async Task<ForexResultTransaction> GetByForexId(ForexId forexId) => await _context.ForexResultTransactions.FirstOrDefaultAsync(x => x.ForexId == forexId);
    }

}
