using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using WalletService.Application.Commands.Purchase;

namespace Activities.Application.IntegrationServices
{
    public class Wallet_StockPurchasedIntegrationHandle : IConsumer<StockPurchasedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public Wallet_StockPurchasedIntegrationHandle(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<StockPurchasedIntegrationEvent> context)
        {
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
