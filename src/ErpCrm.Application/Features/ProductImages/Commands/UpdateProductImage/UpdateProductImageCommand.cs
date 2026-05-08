using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.ProductImages.Commands.UpdateProductImage;

public class UpdateProductImageCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? AltText { get; set; }
    public bool IsMain { get; set; }
    public int SortOrder { get; set; }
}
