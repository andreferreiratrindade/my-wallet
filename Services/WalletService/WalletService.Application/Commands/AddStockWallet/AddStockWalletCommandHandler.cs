using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using WalletService.Domain.Models.Repositories;
using WalletService.Domain.Models.Entities;


namespace WalletService.Application.Commands.Purchase;

public class AddStockWalletCommandHandler : CommandHandler<AddStockWalletCommand, AddStockWalletCommandOutput, AddStockWalletCommandValidation>
{
    private readonly IStockWalletRepository _stockWalletRepository;

    public AddStockWalletCommandHandler(
                                  IStockWalletRepository stockWalletRepository,
                                  IDomainNotification domainNotification,
                                  IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

    {
        _stockWalletRepository = stockWalletRepository;
    }

    public override async Task<AddStockWalletCommandOutput> ExecutCommand(AddStockWalletCommand request, CancellationToken cancellationToken)
    {
        _domainNotification.AddNotification(await CheckValidations(request));
        if (_domainNotification.HasNotifications) return request.GetCommandOutput();

        var stockWallet = await _stockWalletRepository.GetBySymbol(request.Symbol);
        var existsStockWallet = stockWallet != null;

        stockWallet ??= StockWallet.Create(request.Symbol, request.CorrelationId);

        stockWallet.AddStock(request.TransactionStockId,request.Amount, request.CorrelationId);

        if (!existsStockWallet)
        {
            _stockWalletRepository.Add(stockWallet);
        }
        else
        {
            _stockWalletRepository.Update(stockWallet);
        }

        await PersistData(_stockWalletRepository.UnitOfWork);

        return new AddStockWalletCommandOutput
        {
            Amount = stockWallet.Amount,
            Symbol = stockWallet.Symbol,
            TransactionStockId = request.TransactionStockId
        };


    }
    public async Task<List<NotificationMessage>> CheckValidations(AddStockWalletCommand request)
    {
        var lstNotifications = new List<NotificationMessage>();

        return lstNotifications;
    }
}
