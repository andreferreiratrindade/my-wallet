using Framework.Core.Messages;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using ForexService.Domain.Models.Repositories;
using ForexService.Domain.Models.Entities;
using Framework.Core.DomainObjects;
using MediatR;
using ForexService.Domain.Rules;


namespace ForexService.Application.Commands.Purchase
{
    public class PurchaseCommandHandler : CommandHandler<PurchaseCommand, PurchaseCommandOutput, PurchaseCommandValidation>

    {
        private readonly ITransactionForexRepository _transactionForexRepository;
        private readonly IForexResultTransactionRepository _forexResultTransactionRepository;
        private readonly IForexRepository _forexRepository;

        public PurchaseCommandHandler(ITransactionForexRepository transactiontRepository,
                                      IForexResultTransactionRepository forexResultTransactionRepository,
                                      IForexRepository forexRepository,
                                      IDomainNotification domainNotification,
                                      IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _transactionForexRepository = transactiontRepository;
            _forexRepository = forexRepository;
            _forexResultTransactionRepository = forexResultTransactionRepository;
        }

        public override async Task<PurchaseCommandOutput> ExecutCommand(PurchaseCommand request, CancellationToken cancellationToken)
        {
            _domainNotification.AddNotification(await CheckValidations(request));
            if (_domainNotification.HasNotifications) return request.GetCommandOutput();

            var forex = await _forexRepository.GetBySymbol(request.Symbol);

            var transaction = TransactionForex.Purchase(request.Amount,
                                                   request.Value,
                                                   forex.ForexId,
                                                   request.InvestmentDate,
                                                   request.CorrelationId);

            _transactionForexRepository.Add(transaction);

            var forexResultTransaction = await _forexResultTransactionRepository.GetByForexId(forex.ForexId);
            var existsForexResultTransaction = forexResultTransaction != null;

            forexResultTransaction ??= ForexResultTransaction.Create(forex.ForexId,request.CorrelationId);
            forexResultTransaction.Add(request.Amount, request.Value, request.CorrelationId);
            if(!existsForexResultTransaction){
                _forexResultTransactionRepository.Add(forexResultTransaction);
            }else{
                _forexResultTransactionRepository.Update(forexResultTransaction);
            }

            await PersistData(_transactionForexRepository.UnitOfWork);

            return new PurchaseCommandOutput
            {
                TransactionForexId = transaction.TransactionForexId.Value,
                Amount = transaction.Amount,
                Value = transaction.Value,
                Symbol = request.Symbol,
                InvestmentDate = transaction.InvestmentDate
            };


        }
        public async Task<List<NotificationMessage>> CheckValidations(PurchaseCommand request)
        {
            var lstNotifications = new List<NotificationMessage>();

            lstNotifications.AddRange(await BusinessRuleValidation.Check(new ForexExistsRule(request.Symbol, _forexRepository)));
            return lstNotifications;
        }
    }
}
