using FluentValidation;

namespace ForexService.Application.Commands.Sell

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
