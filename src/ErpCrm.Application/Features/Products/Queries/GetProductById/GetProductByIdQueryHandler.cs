using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Products.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Products.Queries.GetProductById;
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IAppDbContext _context;
    public GetProductByIdQueryHandler(IAppDbContext context) => _context = context;
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Products.AsNoTracking().Where(x => x.Id == request.Id).Select(x => new ProductDto { Id = x.Id, Name = x.Name, SKU = x.SKU, Price = x.Price, CostPrice = x.CostPrice, IsPopular = x.IsPopular, IsActive = x.IsActive, CategoryId = x.CategoryId, CategoryName = x.Category.Name, CreatedDate = x.CreatedDate }).FirstOrDefaultAsync(cancellationToken);
        return item is null ? Result<ProductDto>.NotFound("Product not found") : Result<ProductDto>.Ok(item);
    }
}
