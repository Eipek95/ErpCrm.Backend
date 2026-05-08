using FluentValidation;

namespace ErpCrm.Application.Features.Stocks.Commands.UpdateStock;

public class UpdateStockCommandValidator : AbstractValidator<UpdateStockCommand>
{
    public UpdateStockCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ReservedQuantity).GreaterThanOrEqualTo(0);
        RuleFor(x => x).Must(x => x.ReservedQuantity <= x.Quantity).WithMessage("Reserved quantity cannot be greater than quantity.");
    }
}
