using FluentValidation;

namespace ForexService.Application.Commands.Purchase
{
    public class PurchaseCommandValidation : AbstractValidator<PurchaseCommand>
    {
        public PurchaseCommandValidation()
        {
            RuleFor(c => c.Amount)
                .NotEmpty();

        }
    }
}
