using FluentValidation;

namespace WalletService.Application.Commands.Sell

{
    public class SellCommandValidation : AbstractValidator<SellCommand>
    {
        public SellCommandValidation()
        {
            RuleFor(c => c.Amount)
                .NotEmpty();

        }
    }
}
