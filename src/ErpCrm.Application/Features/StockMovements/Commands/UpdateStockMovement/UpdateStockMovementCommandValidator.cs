using FluentValidation;

namespace ErpCrm.Application.Features.StockMovements.Commands.UpdateStockMovement;

public class UpdateStockMovementCommandValidator : AbstractValidator<UpdateStockMovementCommand>
{
    public UpdateStockMovementCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.WarehouseId).GreaterThan(0);
        RuleFor(x => x.MovementType).InclusiveBetween(1, 5);
        RuleFor(x => x.Quantity).NotEqual(0);
        RuleFor(x => x.ReferenceNumber).MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}
