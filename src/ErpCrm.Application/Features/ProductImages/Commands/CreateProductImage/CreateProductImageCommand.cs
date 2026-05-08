using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.ProductImages.Commands.CreateProductImage;

public class CreateProductImageCommand : IRequest<Result<int>>
{
    public int ProductId { get; set; }
    public int? ProductVariantId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? AltText { get; set; }
    public bool IsMain { get; set; }
    public int SortOrder { get; set; }
}
