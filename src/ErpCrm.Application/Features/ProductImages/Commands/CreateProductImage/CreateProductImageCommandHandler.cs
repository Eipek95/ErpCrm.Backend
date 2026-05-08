using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.ProductImages.Commands.CreateProductImage;

public class CreateProductImageCommandHandler : IRequestHandler<CreateProductImageCommand, Result<int>>
{
    private readonly IAppDbContext _context;

    public CreateProductImageCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
    {
        var productExists = await _context.Products.AnyAsync(x => x.Id == request.ProductId, cancellationToken);
        if (!productExists)
            return Result<int>.Fail("Product not found");

        if (request.ProductVariantId.HasValue)
        {
            var variantExists = await _context.ProductVariants.AnyAsync(x =>
                x.Id == request.ProductVariantId.Value &&
                x.ProductId == request.ProductId, cancellationToken);

            if (!variantExists)
                return Result<int>.Fail("Product variant not found");
        }

        if (request.IsMain)
        {
            var oldMainImages = await _context.ProductImages
                .Where(x => x.ProductId == request.ProductId && x.ProductVariantId == request.ProductVariantId && x.IsMain)
                .ToListAsync(cancellationToken);

            foreach (var image in oldMainImages)
                image.IsMain = false;
        }

        var entity = new ProductImage
        {
            ProductId = request.ProductId,
            ProductVariantId = request.ProductVariantId,
            ImageUrl = request.ImageUrl,
            AltText = request.AltText,
            IsMain = request.IsMain,
            SortOrder = request.SortOrder,
            CreatedDate = DateTime.UtcNow
        };

        await _context.ProductImages.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Created(entity.Id, "Product image created successfully");
    }
}
