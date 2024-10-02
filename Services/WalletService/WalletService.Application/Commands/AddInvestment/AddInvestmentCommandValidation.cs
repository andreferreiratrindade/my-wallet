using FluentValidation;

namespace WalletService.Application.Commands.Purchase
{
    public class AddInvestmentCommandValidation : AbstractValidator<AddInvestmentCommand>
    {
        public PurchaseCommandValidation()
        {
            RuleFor(c => c.Amount)
                .NotEmpty();

        }
    }
}
