using FluentValidation;

namespace ErpCrm.Application.Features.ProductImages.Commands.UpdateProductImage;

public class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageCommand>
{
    public UpdateProductImageCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.AltText).MaximumLength(250);
        RuleFor(x => x.SortOrder).GreaterThanOrEqualTo(0);
    }
}
