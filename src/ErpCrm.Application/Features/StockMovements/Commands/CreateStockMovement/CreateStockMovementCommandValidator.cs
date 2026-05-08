using FluentValidation;

namespace ErpCrm.Application.Features.StockMovements.Commands.CreateStockMovement;

public class CreateStockMovementCommandValidator : AbstractValidator<CreateStockMovementCommand>
{
    public CreateStockMovementCommandValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.WarehouseId).GreaterThan(0);
        RuleFor(x => x.MovementType).InclusiveBetween(1, 5);
        RuleFor(x => x.Quantity).NotEqual(0);
        RuleFor(x => x.ReferenceNumber).MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}
