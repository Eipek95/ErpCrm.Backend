using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.ProductVariants.Commands.DeleteProductVariant;

public class DeleteProductVariantCommandHandler : IRequestHandler<DeleteProductVariantCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public DeleteProductVariantCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteProductVariantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductVariants.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            return Result<bool>.NotFound("Product variant not found");

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Product variant deleted successfully");
    }
}
