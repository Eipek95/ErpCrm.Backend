using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.ProductImages.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.ProductImages.Queries.GetProductImageById;

public class GetProductImageByIdQueryHandler : IRequestHandler<GetProductImageByIdQuery, Result<ProductImageDto>>
{
    private readonly IAppDbContext _context;

    public GetProductImageByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductImageDto>> Handle(GetProductImageByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.ProductImages
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new ProductImageDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductVariantId = x.ProductVariantId,
                ImageUrl = x.ImageUrl,
                AltText = x.AltText,
                IsMain = x.IsMain,
                SortOrder = x.SortOrder
            })
            .FirstOrDefaultAsync(cancellationToken);

        return item is null
            ? Result<ProductImageDto>.NotFound("Product image not found")
            : Result<ProductImageDto>.Ok(item);
    }
}
