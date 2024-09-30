using Framework.Core.Data;
using Framework.Core.Mediator;
using Microsoft.EntityFrameworkCore;
using StockService.Domain.Models.Entities;

namespace StockService.Infra
{
    public class StockContext : DbContextCustom<StockContext>
    {

        public StockContext(DbContextOptions<StockContext> options, IEventStored eventStored, IMediatorHandler mediatorHandler)
           : base(options,  eventStored,mediatorHandler)
        {
        }


        public DbSet<Transaction> Transations { get; set; }

    }

}
