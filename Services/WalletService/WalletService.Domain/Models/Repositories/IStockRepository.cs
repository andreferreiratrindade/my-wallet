using Framework.Core.Data;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Entities.Ids;

namespace WalletService.Domain.Models.Repositories
{
    public interface IWalletRepository : IRepository<Wallet>
    {

        Task<Wallet> GetById(WalletId WalletId);

        Task<Wallet> GetBySymbol(string symbol);
    }
}
