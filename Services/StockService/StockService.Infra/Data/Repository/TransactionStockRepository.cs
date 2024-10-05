using System.Data.Common;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Entities.Ids;
using StockService.Domain.Models.Repositories;

namespace StockService.Infra.Data.Repository
{
    public class TransactionStockRepository : ITransactionStockRepository
    {
        private readonly StockContext _context;

        public TransactionStockRepository(StockContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(TransactionStock transaction)
        {
            _context.TransactionsStock.Add(transaction);
        }

        public void Update(TransactionStock transaction)
        {
            _context.TransactionsStock.Update(transaction);

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



        public Task<TransactionStock?> GetByIdAsync(Guid activityId)
        {
            throw new NotImplementedException();
        }
       public  async Task<TransactionStock?> GetById(TransactionStockId transactionStockId){
           return await _context.TransactionsStock.FirstOrDefaultAsync(x=> x.TransactionStockId == transactionStockId);
        }


        public IQueryable<TransactionStock> GetQueryable()
        {
            throw new NotImplementedException();
        }

   }

}
