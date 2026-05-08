using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.ProductVariants.Commands.CreateProductVariant;

public class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand, Result<int>>
{
    private readonly IAppDbContext _context;

    public CreateProductVariantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
    {
        var productExists = await _context.Products.AnyAsync(x => x.Id == request.ProductId, cancellationToken);
        if (!productExists)
            return Result<int>.Fail("Product not found");

        var codeExists = await _context.ProductVariants.AnyAsync(x => x.VariantCode == request.VariantCode, cancellationToken);
        if (codeExists)
            return Result<int>.Fail("Variant code already exists", 409);

        var entity = new ProductVariant
        {
            ProductId = request.ProductId,
            VariantCode = request.VariantCode,
            Color = request.Color,
            Size = request.Size,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            IsActive = request.IsActive,
            CreatedDate = DateTime.UtcNow
        };

        await _context.ProductVariants.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Created(entity.Id, "Product variant created successfully");
    }
}
