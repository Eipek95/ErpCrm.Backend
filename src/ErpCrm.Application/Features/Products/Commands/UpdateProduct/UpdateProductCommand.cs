using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string SKU { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal CostPrice { get; set; }
    public bool IsPopular { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
}
