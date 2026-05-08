using FluentValidation;

namespace ErpCrm.Application.Features.ProductImages.Commands.CreateProductImage;

public class CreateProductImageCommandValidator : AbstractValidator<CreateProductImageCommand>
{
    public CreateProductImageCommandValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.ProductVariantId).GreaterThan(0).When(x => x.ProductVariantId.HasValue);
        RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.AltText).MaximumLength(250);
        RuleFor(x => x.SortOrder).GreaterThanOrEqualTo(0);
    }
}
