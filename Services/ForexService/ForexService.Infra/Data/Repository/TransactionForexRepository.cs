using System.Data.Common;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Entities.Ids;
using ForexService.Domain.Models.Repositories;

namespace ForexService.Infra.Data.Repository
{
    public class TransactionForexRepository : ITransactionForexRepository
    {
        private readonly ForexContext _context;

        public TransactionForexRepository(ForexContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(TransactionForex transaction)
        {
            _context.TransactionsForex.Add(transaction);
        }

        public void Update(TransactionForex transaction)
        {
            _context.TransactionsForex.Update(transaction);

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



        public Task<TransactionForex?> GetByIdAsync(Guid activityId)
        {
            throw new NotImplementedException();
        }
       public  async Task<TransactionForex?> GetById(TransactionForexId transactionForexId){
           return await _context.TransactionsForex.FirstOrDefaultAsync(x=> x.TransactionForexId == transactionForexId);
        }


        public IQueryable<TransactionForex> GetQueryable()
        {
            throw new NotImplementedException();
        }

   }

}
