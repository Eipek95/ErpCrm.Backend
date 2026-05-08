using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.ProductVariants.Commands.UpdateProductVariant;

public class UpdateProductVariantCommandHandler : IRequestHandler<UpdateProductVariantCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public UpdateProductVariantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductVariants.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            return Result<bool>.NotFound("Product variant not found");

        var productExists = await _context.Products.AnyAsync(x => x.Id == request.ProductId, cancellationToken);
        if (!productExists)
            return Result<bool>.Fail("Product not found");

        var codeExists = await _context.ProductVariants.AnyAsync(x => x.Id != request.Id && x.VariantCode == request.VariantCode, cancellationToken);
        if (codeExists)
            return Result<bool>.Fail("Variant code already exists", 409);

        entity.ProductId = request.ProductId;
        entity.VariantCode = request.VariantCode;
        entity.Color = request.Color;
        entity.Size = request.Size;
        entity.Price = request.Price;
        entity.StockQuantity = request.StockQuantity;
        entity.IsActive = request.IsActive;
        entity.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Product variant updated successfully");
    }
}
