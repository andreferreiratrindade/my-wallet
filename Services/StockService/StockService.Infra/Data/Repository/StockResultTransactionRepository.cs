using System.Data.Common;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Entities.Ids;
using StockService.Domain.Models.Repositories;

namespace StockService.Infra.Data.Repository
{
    public class StockResultTransactionStockRepository : IStockResultTransactionStockRepository
    {
        private readonly StockContext _context;

        public StockResultTransactionStockRepository(StockContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(StockResultTransaction entity)
        {
            _context.StockResultTransactions.Add(entity);
        }

        public void Update(StockResultTransaction entity)
        {
            _context.StockResultTransactions.Update(entity);

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

        public StockResultTransactionStockRepository? GetById(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<StockResultTransactionStockRepository> GetQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<StockResultTransaction> GetBySymbolAsync(string Symbol)
        {
            throw new NotImplementedException();
        }



        public async Task<StockResultTransaction> GetByStockId(StockId stockId) => await _context.StockResultTransactions.FirstOrDefaultAsync(x => x.StockId == stockId);
    }

}
