using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Products.Commands.DeleteProduct;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
{
    private readonly IAppDbContext _context;
    public DeleteProductCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null) return Result<bool>.NotFound("Product not found");
        entity.IsDeleted = true; entity.DeletedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Ok(true, "Product deleted successfully");
    }
}
