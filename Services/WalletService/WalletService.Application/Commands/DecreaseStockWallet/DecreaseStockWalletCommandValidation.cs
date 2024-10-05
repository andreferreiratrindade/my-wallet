using FluentValidation;

namespace WalletService.Application.Commands.Sell

{
    public class DecreaseStockWalletCommandValidation : AbstractValidator<DecreaseStockWalletCommand>
    {
        public DecreaseStockWalletCommandValidation()
        {
            RuleFor(c => c.Amount)
                .NotEmpty();

            RuleFor(c => c.Symbol)
                .NotEmpty();

            RuleFor(c => c.TransactionStockId)
                .NotEmpty();

        }
    }
}
