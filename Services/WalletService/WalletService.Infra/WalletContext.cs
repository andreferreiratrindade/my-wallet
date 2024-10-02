using Framework.Core.Data;
using Framework.Core.Mediator;
using Microsoft.EntityFrameworkCore;
using WalletService.Domain.Models.Entities;

namespace WalletService.Infra
{
    public class WalletContext : DbContextCustom<WalletContext>
    {

        public WalletContext(DbContextOptions<WalletContext> options, IEventStored eventStored, IMediatorHandler mediatorHandler)
           : base(options,  eventStored,mediatorHandler)
        {
        }


        public DbSet<Transaction> Transations { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletResultTransaction>  WalletResultTransactions{ get; set; }

    }

}
