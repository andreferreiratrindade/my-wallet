using System.Data.Common;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Entities.Ids;
using ForexService.Domain.Models.Repositories;

namespace ForexService.Infra.Data.Repository
{
    public class ForexRepository : IForexRepository
    {
        private readonly ForexContext _context;
        private readonly IMemoryCache _memoryCache;

        public ForexRepository(ForexContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public IUnitOfWork UnitOfWork => _context;


        public void Dispose()
        {
            _context.Dispose();
        }

        public  Task<Forex?> GetById(ForexId ForexId)
        {
            return  _memoryCache.GetOrCreate(ForexId,  entry =>
         {
             return  _context.Forexs.FirstOrDefaultAsync(x => x.ForexId == ForexId);
         });
        }

        public  Task<Forex> GetBySymbol(string symbol)
        {
            return  _memoryCache.GetOrCreate(symbol,  entry =>
        {
            return  _context.Forexs.FirstOrDefaultAsync(x => x.Symbol == symbol);
        });
        }

        public DbConnection GetConnection() => _context.Database.GetDbConnection();


    }

}
