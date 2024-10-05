using Framework.Core.DomainObjects;
using Framework.Core.LogHelpers;
using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockService.Application.Commands.Purchase;
using StockService.Domain.Models.Entities.Ids;

namespace StockService.Application.IntegrationServices
{
    public class Stock_StockWalletIntegrationHandle :
    IConsumer<StockWalletAddedConfirmedIntegrationEvent>,
    IConsumer<StockWalletDecreasedConfirmedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Stock_StockWalletIntegrationHandle> _logger;

        public Stock_StockWalletIntegrationHandle(IServiceProvider serviceProvider, ILogger<Stock_StockWalletIntegrationHandle> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<StockWalletAddedConfirmedIntegrationEvent> context)
        {
           _logger.CreateLog(new GenericLog(context.Message.CorrelationId,
                                             context.Message.GetType().Name,
                                             [LogConstants.RECEIVE_FROM_BROKER],
                                             context.Message));
            await ExecuteConfirmStock(context.Message.TransactionStokId, context.Message.CorrelationId);
        }
         public async Task Consume(ConsumeContext<StockWalletDecreasedConfirmedIntegrationEvent> context)
        {
            _logger.CreateLog(new GenericLog(context.Message.CorrelationId,
                                             context.Message.GetType().Name,
                                             [LogConstants.RECEIVE_FROM_BROKER],
                                             context.Message));
            await ExecuteConfirmStock(context.Message.TransactionStokId, context.Message.CorrelationId);

        }

        public async Task ExecuteConfirmStock(Guid transactionStockId, CorrelationId correlationId){
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            _ = await mediator.SendCommand<ConfirmCommand, ConfirmCommandOutput>(
                    new ConfirmCommand(new TransactionStockId(transactionStockId),correlationId));
        }
    }
}
