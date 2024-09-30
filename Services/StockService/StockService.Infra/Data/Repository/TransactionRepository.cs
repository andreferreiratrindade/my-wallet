using System.Data.Common;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Repositories;

namespace StockService.Infra.Data.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly StockContext _context;

        public TransactionRepository(StockContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(Transaction transaction)
        {
            _context.Transations.Add(transaction);
        }

        public void Update(Transaction transaction)
        {
            _context.Transations.Update(transaction);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public DbConnection GetConnection() => _context.Database.GetDbConnection();

        public void Delete(Guid activityId)
        {
            throw new NotImplementedException();
        }

        public Transaction? GetById(Guid activityId)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction?> GetByIdAsync(Guid activityId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Transaction> GetQueryable()
        {
            throw new NotImplementedException();
        }

        // public IQueryable<Transaction> GetQueryable()
        // {
        //     return _context.Activities.AsQueryable();
        // }

        // public void Delete(Guid activityId)
        // {
        //     _context.Activities.Where(x => x.AggregateId == activityId).ExecuteDelete();
        // }

        //   public Transaction? GetById(Guid activityId) => _context.Activities.AsNoTracking().FirstOrDefault(x => x.AggregateId == activityId && x.Status != TypeActivityStatus.Deleted);
        // public async Task<Activity?> GetByIdAsync(Guid activityId) => await _context.Activities.AsNoTracking().FirstOrDefaultAsync(x => x.AggregateId == activityId && x.Status != TypeActivityStatus.Deleted);
    }

}
