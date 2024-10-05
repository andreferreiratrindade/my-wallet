using System.Data.Common;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Entities.Ids;
using WalletService.Domain.Models.Repositories;

namespace WalletService.Infra.Data.Repository
{
    public class StockWalletRepository : IStockWalletRepository
    {
        private readonly WalletContext _context;

        public StockWalletRepository(WalletContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

       public void Add(StockWallet entity)
        {
            _context.StockWallets.Add(entity);
        }

        public void Update(StockWallet entity)
        {
            _context.StockWallets.Update(entity);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public  async Task<StockWallet> GetById(StockWalletId StockWalletId)
        {

             return  await _context.StockWallets.FirstOrDefaultAsync(x => x.StockWalletId == StockWalletId);

        }

        public  async Task<StockWallet> GetBySymbol(string symbol)
        {
            return await  _context.StockWallets.FirstOrDefaultAsync(x => x.Symbol == symbol);
        }

        public DbConnection GetConnection() => _context.Database.GetDbConnection();

    }

}
