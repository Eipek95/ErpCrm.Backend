using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.ProductVariants.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.ProductVariants.Queries.GetProductVariantById;

public class GetProductVariantByIdQueryHandler : IRequestHandler<GetProductVariantByIdQuery, Result<ProductVariantDto>>
{
    private readonly IAppDbContext _context;

    public GetProductVariantByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductVariantDto>> Handle(GetProductVariantByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.ProductVariants
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new ProductVariantDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                VariantCode = x.VariantCode,
                Color = x.Color,
                Size = x.Size,
                Price = x.Price,
                StockQuantity = x.StockQuantity,
                IsActive = x.IsActive,
                CreatedDate = x.CreatedDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        return item is null
            ? Result<ProductVariantDto>.NotFound("Product variant not found")
            : Result<ProductVariantDto>.Ok(item);
    }
}
