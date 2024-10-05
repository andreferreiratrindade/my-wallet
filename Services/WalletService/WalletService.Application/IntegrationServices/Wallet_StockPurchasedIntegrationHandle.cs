using Framework.Core.LogHelpers;
using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WalletService.Application.Commands.Purchase;

namespace WalletService.Application.IntegrationServices

{
    public class Wallet_StockPurchasedIntegrationHandle : IConsumer<StockPurchasedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Wallet_StockPurchasedIntegrationHandle> _logger;


        public Wallet_StockPurchasedIntegrationHandle(IServiceProvider serviceProvider, ILogger<Wallet_StockPurchasedIntegrationHandle> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

        }

        public async Task Consume(ConsumeContext<StockPurchasedIntegrationEvent> context)
        {
            _logger.CreateLog(new GenericLog(context.Message.CorrelationId,
                                             context.Message.GetType().Name,
                                             [LogConstants.RECEIVE_FROM_BROKER],
                                             context.Message));

            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            _ = await mediator.SendCommand<AddStockWalletCommand, AddStockWalletCommandOutput>(
                    new AddStockWalletCommand(
                        context.Message.TransactionStockId,
                        context.Message.Amount,
                        context.Message.Symbol,
                        context.Message.CorrelationId));

        }
    }
}
