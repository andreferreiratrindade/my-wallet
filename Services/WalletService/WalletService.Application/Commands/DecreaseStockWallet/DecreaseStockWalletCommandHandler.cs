using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using WalletService.Domain.Models.Repositories;
using Framework.Core.DomainObjects;
using WalletService.Domain.Models.Entities;


namespace WalletService.Application.Commands.Sell
{
    public class DecreaseStockWalletCommandHandler : CommandHandler<DecreaseStockWalletCommand, DecreaseStockWalletCommandOutput, DecreaseStockWalletCommandValidation>

    {
        private readonly IStockWalletRepository _stockWalletRepository;

        public DecreaseStockWalletCommandHandler(IStockWalletRepository stockWalletRepository,
                                      IDomainNotification domainNotification,
                                      IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _stockWalletRepository = stockWalletRepository;
        }

        public override async Task<DecreaseStockWalletCommandOutput> ExecutCommand(DecreaseStockWalletCommand request, CancellationToken cancellationToken)
        {
           _domainNotification.AddNotification(await CheckValidations(request));
        if (_domainNotification.HasNotifications) return request.GetCommandOutput();

        var stockWallet = await _stockWalletRepository.GetBySymbol(request.Symbol);
        var existsStockWallet = stockWallet != null;

        stockWallet ??= StockWallet.Create(request.Symbol, request.CorrelationId);

        stockWallet.DecreaseStock(request.TransactionStockId,request.Amount, request.CorrelationId);

        if (existsStockWallet)
        {
            _stockWalletRepository.Add(stockWallet);
        }
        else
        {
            _stockWalletRepository.Update(stockWallet);
        }

        await PersistData(_stockWalletRepository.UnitOfWork);

        return new DecreaseStockWalletCommandOutput
        {
            Amount = stockWallet.Amount,
            Symbol = stockWallet.Symbol,
            TransactionStockId = request.TransactionStockId
        };

        }
        public  async  Task<List<NotificationMessage>> CheckValidations(DecreaseStockWalletCommand request)
        {

            List<NotificationMessage> lstNotifications = new();

            return lstNotifications;
        }
    }
}
