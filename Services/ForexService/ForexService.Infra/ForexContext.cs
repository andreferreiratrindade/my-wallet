using System.Data;
using ForexService.Domain.Models.Entities;
using Framework.Core.Data;
using Framework.Core.Mediator;
using Microsoft.EntityFrameworkCore;

namespace ForexService.Infra
{
    public class ForexContext : DbContextCustom<ForexContext>
    {

        public ForexContext(DbContextOptions<ForexContext> options, IEventStored eventStored, IMediatorHandler mediatorHandler)
           : base(options,  eventStored,mediatorHandler)
        {
        }


        public DbSet<TransactionForex> TransactionsForex { get; set; }
        public DbSet<Forex> Forexs { get; set; }
        public DbSet<ForexResultTransaction>  ForexResultTransactions{ get; set; }


public  void LoadForexList()
    {
        var forexs = new List<Forex>
        {
            Forex.Create("ITAU", "ITSA4"),
            Forex.Create("Ambev", "ABEV3"),
            Forex.Create("Usiminas", "USIM4")
        };

        var newForexs = forexs.Where(x=> !Forexs.Select(y=>y.Symbol).ToList().Any(y=> y == x.Symbol)).ToList();

        Forexs.AddRange(newForexs);
        this.SaveChanges();
    }
    }

}
