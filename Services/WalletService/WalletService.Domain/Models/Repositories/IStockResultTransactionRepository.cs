using Framework.Core.Data;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Entities.Ids;

namespace WalletService.Domain.Models.Repositories
{
    public interface IWalletResultTransactionRepository : IRepository<Transaction>
    {
        void Update(WalletResultTransaction walletResultTransaction);
        void Add(WalletResultTransaction walletResultTransaction);
        void Delete(Guid walletId);
        Task<WalletResultTransaction>  GetByWalletId(WalletId walletId);


    }
}
