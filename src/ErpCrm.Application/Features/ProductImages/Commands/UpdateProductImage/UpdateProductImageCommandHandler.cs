using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.ProductImages.Commands.UpdateProductImage;

public class UpdateProductImageCommandHandler : IRequestHandler<UpdateProductImageCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public UpdateProductImageCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            return Result<bool>.NotFound("Product image not found");

        if (request.IsMain)
        {
            var oldMainImages = await _context.ProductImages
                .Where(x =>
                    x.Id != request.Id &&
                    x.ProductId == entity.ProductId &&
                    x.ProductVariantId == entity.ProductVariantId &&
                    x.IsMain)
                .ToListAsync(cancellationToken);

            foreach (var image in oldMainImages)
                image.IsMain = false;
        }

        entity.ImageUrl = request.ImageUrl;
        entity.AltText = request.AltText;
        entity.IsMain = request.IsMain;
        entity.SortOrder = request.SortOrder;
        entity.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Product image updated successfully");
    }
}
