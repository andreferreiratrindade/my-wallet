using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using WalletService.Domain.Models.Repositories;
using WalletService.Domain.Models.Entities;
using Framework.Core.DomainObjects;
using Activities.Domain.Rules;
using MediatR;


namespace WalletService.Application.Commands.Sell
{
    public class SellCommandHandler : CommandHandler<SellCommand, SellCommandOutput, SellCommandValidation>

    {
        private readonly ITransactionRepository _transactiontRepository;
        private readonly IWalletResultTransactionRepository _walletResultTransactionRepository;
        private readonly IWalletRepository _walletRepository;

        public SellCommandHandler(ITransactionRepository transactiontRepository,
                                      IWalletResultTransactionRepository walletResultTransactionRepository,
                                      IWalletRepository walletRepository,
                                      IDomainNotification domainNotification,
                                      IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _transactiontRepository = transactiontRepository;
            _walletRepository = walletRepository;
            _walletResultTransactionRepository = walletResultTransactionRepository;
        }

        public override async Task<SellCommandOutput> ExecutCommand(SellCommand request, CancellationToken cancellationToken)
        {
            _domainNotification.AddNotification(await CheckValidations(request));
            if (_domainNotification.HasNotifications) return request.GetCommandOutput();

            var wallet = await _walletRepository.GetBySymbol(request.Symbol);

            var transaction = Transaction.Sell(request.Amount,
                                                   request.Value,
                                                   wallet.WalletId,
                                                   request.InvestmentDate,
                                                   request.CorrelationId);

            _transactiontRepository.Add(transaction);

            var walletResultTransaction = await _walletResultTransactionRepository.GetByWalletId(wallet.WalletId);
            var existsWalletResultTransaction = walletResultTransaction != null;

            walletResultTransaction ??= WalletResultTransaction.Create(wallet.WalletId,request.CorrelationId);
            walletResultTransaction.Decrease(request.Amount, request.Value, request.CorrelationId);
            if(!existsWalletResultTransaction){
                _walletResultTransactionRepository.Add(walletResultTransaction);
            }else{
                _walletResultTransactionRepository.Update(walletResultTransaction);
            }

            await PersistData(_transactiontRepository.UnitOfWork);

            return new SellCommandOutput
            {
                TransactionId = transaction.TransactionId.Value,
                Amount = transaction.Amount,
                Value = transaction.Value,
                Symbol = request.Symbol,
                InvestmentDate = transaction.InvestmentDate
            };


        }
        public  async  Task<List<NotificationMessage>> CheckValidations(SellCommand request)
        {

            List<NotificationMessage> lstNotifications = new();

            lstNotifications.AddRange( await BusinessRuleValidation.Check(new WalletExistsRule(request.Symbol, _walletRepository)));
            lstNotifications.AddRange( await BusinessRuleValidation.Check(new HasWalletAmountEnoughToSellRule(request.Symbol, request.Amount, _walletRepository, _walletResultTransactionRepository)));
            return lstNotifications;
        }
    }
}
