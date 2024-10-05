using Framework.Core.Data;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Entities.Ids;

namespace WalletService.Domain.Models.Repositories
{
    public interface IStockWalletRepository : IRepository<StockWallet>
    {

        Task<StockWallet> GetById(StockWalletId StockWalletId);

        Task<StockWallet> GetBySymbol(string symbol);

          void Update(StockWallet stockWallet);
        void Add(StockWallet stockWallet);
    }
}
