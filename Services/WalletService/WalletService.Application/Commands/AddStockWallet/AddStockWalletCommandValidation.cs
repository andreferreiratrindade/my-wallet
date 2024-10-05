using FluentValidation;

namespace WalletService.Application.Commands.Purchase
{
    public class AddStockWalletCommandValidation : AbstractValidator<AddStockWalletCommand>
    {
        public AddStockWalletCommandValidation()
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
