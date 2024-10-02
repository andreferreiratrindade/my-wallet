using MediatR;
using MassTransit;
using Framework.MessageBus;
using WalletService.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using WalletService.Domain.Models.Repositories;

namespace WalletService.Application.Events
{
    public class TransactionSoldEventHandler : INotificationHandler<TransactionSoldEvent>
    {
        private readonly IMessageBus _messageBus;
        private readonly IWalletRepository _walletRepository;
        public TransactionSoldEventHandler(IMessageBus messageBus, IWalletRepository walletRepository)
        {
            _messageBus = messageBus;
            _walletRepository =walletRepository;
        }

        public async Task Handle(TransactionSoldEvent message, CancellationToken cancellationToken)
        {
            var symbolWallet = await _walletRepository.GetById(message.WalletId);
            await _messageBus.PublishAsync(
                       new WalletSoldIntegrationEvent(message.TransactionId.Value,
                                                     message.Amount ,
                                                      message.Value,
                                                      symbolWallet.Symbol,
                                                      message.InvestmentDate,
                                                      message.TypeOperationInvestment,
                                                      message.CorrelationId),cancellationToken);
        }
    }
}
