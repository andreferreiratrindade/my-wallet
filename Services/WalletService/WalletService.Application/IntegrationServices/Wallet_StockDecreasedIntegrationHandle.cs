using Amazon.Runtime.Internal.Util;
using Framework.Core.LogHelpers;
using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WalletService.Application.Commands.Purchase;
using WalletService.Application.Commands.Sell;

namespace WalletService.Application.IntegrationServices
{
    public class Wallet_StockDecreasedIntegrationHandle : IConsumer<StockSoldIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Wallet_StockDecreasedIntegrationHandle> _logger;

        public Wallet_StockDecreasedIntegrationHandle(IServiceProvider serviceProvider, ILogger<Wallet_StockDecreasedIntegrationHandle> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<StockSoldIntegrationEvent> context)
        {
            _logger.CreateLog(new GenericLog(context.Message.CorrelationId,
                                             context.Message.GetType().Name,
                                             [LogConstants.RECEIVE_FROM_BROKER],
                                             context.Message));
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            _ = await mediator.SendCommand<DecreaseStockWalletCommand, DecreaseStockWalletCommandOutput>(
                    new DecreaseStockWalletCommand(
                        context.Message.TransactionStockId,
                        context.Message.Amount,
                        context.Message.Symbol,
                        context.Message.CorrelationId));

        }
    }
}
