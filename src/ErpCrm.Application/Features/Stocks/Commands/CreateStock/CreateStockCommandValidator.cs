using FluentValidation;

namespace ErpCrm.Application.Features.Stocks.Commands.CreateStock;

public class CreateStockCommandValidator : AbstractValidator<CreateStockCommand>
{
    public CreateStockCommandValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.ProductVariantId).GreaterThan(0).When(x => x.ProductVariantId.HasValue);
        RuleFor(x => x.WarehouseId).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ReservedQuantity).GreaterThanOrEqualTo(0);
        RuleFor(x => x).Must(x => x.ReservedQuantity <= x.Quantity).WithMessage("Reserved quantity cannot be greater than quantity.");
    }
}
