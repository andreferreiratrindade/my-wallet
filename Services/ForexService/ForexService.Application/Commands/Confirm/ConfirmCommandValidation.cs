using FluentValidation;

namespace ForexService.Application.Commands.Purchase
{
    public class ConfirmCommandValidation : AbstractValidator<ConfirmCommand>
    {
        public ConfirmCommandValidation()
        {
            RuleFor(c => c.TransactionForexId)
                .NotEmpty();

        }
    }
}
