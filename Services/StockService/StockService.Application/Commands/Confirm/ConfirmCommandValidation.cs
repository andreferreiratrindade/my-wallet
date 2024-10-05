using FluentValidation;

namespace StockService.Application.Commands.Purchase
{
    public class ConfirmCommandValidation : AbstractValidator<ConfirmCommand>
    {
        public ConfirmCommandValidation()
        {
            RuleFor(c => c.TransactionStockId)
                .NotEmpty();

        }
    }
}
