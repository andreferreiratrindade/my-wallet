using System.Data.Common;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Entities.Ids;
using WalletService.Domain.Models.Repositories;

namespace WalletService.Infra.Data.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletContext _context;
        private readonly IMemoryCache _memoryCache;

        public WalletRepository(WalletContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public IUnitOfWork UnitOfWork => _context;


        public void Dispose()
        {
            _context.Dispose();
        }

        public  Task<Wallet?> GetById(WalletId WalletId)
        {
            return  _memoryCache.GetOrCreate(WalletId,  entry =>
         {
             return  _context.Wallets.FirstOrDefaultAsync(x => x.WalletId == WalletId);
         });
        }

        public  Task<Wallet> GetBySymbol(string symbol)
        {
            return  _memoryCache.GetOrCreate(symbol,  entry =>
        {
            return  _context.Wallets.FirstOrDefaultAsync(x => x.Symbol == symbol);
        });
        }

        public DbConnection GetConnection() => _context.Database.GetDbConnection();


    }

}
