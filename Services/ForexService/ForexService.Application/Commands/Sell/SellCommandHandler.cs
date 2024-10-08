using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using ForexService.Domain.Models.Repositories;
using ForexService.Domain.Models.Entities;
using Framework.Core.DomainObjects;
using MediatR;
using ForexService.Domain.Rules;


namespace ForexService.Application.Commands.Sell
{
    public class SellCommandHandler : CommandHandler<SellCommand, SellCommandOutput, SellCommandValidation>

    {
        private readonly ITransactionForexRepository _transactionForexRepository;
        private readonly IForexResultTransactionRepository _forexResultTransactionRepository;
        private readonly IForexRepository _forexRepository;

        public SellCommandHandler(ITransactionForexRepository transactiontRepository,
                                      IForexResultTransactionRepository forexResultTransactionRepository,
                                      IForexRepository forexRepository,
                                      IDomainNotification domainNotification,
                                      IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _transactionForexRepository = transactiontRepository;
            _forexRepository = forexRepository;
            _forexResultTransactionRepository = forexResultTransactionRepository;
        }

        public override async Task<SellCommandOutput> ExecutCommand(SellCommand request, CancellationToken cancellationToken)
        {
            _domainNotification.AddNotification(await CheckValidations(request));
            if (_domainNotification.HasNotifications) return request.GetCommandOutput();

            var forex = await _forexRepository.GetBySymbol(request.Symbol);

            var transaction = TransactionForex.Sell(request.Amount,
                                                   request.Value,
                                                   forex.ForexId,
                                                   request.InvestmentDate,
                                                   request.CorrelationId);

            _transactionForexRepository.Add(transaction);

            var forexResultTransaction = await _forexResultTransactionRepository.GetByForexId(forex.ForexId);
            forexResultTransaction.Decrease(request.Amount, request.Value, request.CorrelationId);

            _forexResultTransactionRepository.Update(forexResultTransaction);

            await PersistData(_transactionForexRepository.UnitOfWork);

            return new SellCommandOutput
            {
                TransactionForexId = transaction.TransactionForexId.Value,
                Amount = transaction.Amount,
                Value = transaction.Value,
                Symbol = request.Symbol,
                InvestmentDate = transaction.InvestmentDate
            };


        }
        public  async  Task<List<NotificationMessage>> CheckValidations(SellCommand request)
        {

            List<NotificationMessage> lstNotifications = new();

            lstNotifications.AddRange( await BusinessRuleValidation.Check(new ForexExistsRule(request.Symbol, _forexRepository)));
            lstNotifications.AddRange( await BusinessRuleValidation.Check(new HasForexAmountEnoughToSellRule(request.Symbol, request.Amount, _forexRepository, _forexResultTransactionRepository)));
            return lstNotifications;
        }
    }
}
