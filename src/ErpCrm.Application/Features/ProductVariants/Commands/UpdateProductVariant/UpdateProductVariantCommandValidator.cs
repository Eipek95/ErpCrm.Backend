using FluentValidation;

namespace ErpCrm.Application.Features.ProductVariants.Commands.UpdateProductVariant;

public class UpdateProductVariantCommandValidator : AbstractValidator<UpdateProductVariantCommand>
{
    public UpdateProductVariantCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.VariantCode).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Color).MaximumLength(80);
        RuleFor(x => x.Size).MaximumLength(50);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).When(x => x.Price.HasValue);
        RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0);
    }
}
