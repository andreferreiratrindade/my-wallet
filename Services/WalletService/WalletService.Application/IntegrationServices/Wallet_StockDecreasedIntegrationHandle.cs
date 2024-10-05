using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using WalletService.Application.Commands.Purchase;
using WalletService.Application.Commands.Sell;

namespace Activities.Application.IntegrationServices
{
    public class Wallet_StockDecreasedIntegrationHandle : IConsumer<StockSoldIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public Wallet_StockDecreasedIntegrationHandle(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<StockSoldIntegrationEvent> context)
        {
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
