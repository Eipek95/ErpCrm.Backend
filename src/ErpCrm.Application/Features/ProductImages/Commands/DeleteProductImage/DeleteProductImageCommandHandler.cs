using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.ProductImages.Commands.DeleteProductImage;

public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public DeleteProductImageCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            return Result<bool>.NotFound("Product image not found");

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Product image deleted successfully");
    }
}
