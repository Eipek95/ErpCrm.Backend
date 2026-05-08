using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.ProductVariants.Commands.UpdateProductVariant;

public class UpdateProductVariantCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string VariantCode { get; set; } = null!;
    public string? Color { get; set; }
    public string? Size { get; set; }
    public decimal? Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
}
