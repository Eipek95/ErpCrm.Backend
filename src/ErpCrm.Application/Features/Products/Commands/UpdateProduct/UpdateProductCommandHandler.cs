using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<bool>>
{
    private readonly IAppDbContext _context;
    public UpdateProductCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null) return Result<bool>.NotFound("Product not found");
        entity.Name = request.Name; entity.SKU = request.SKU; entity.Price = request.Price; entity.CostPrice = request.CostPrice; entity.IsPopular = request.IsPopular; entity.IsActive = request.IsActive; entity.CategoryId = request.CategoryId;
        entity.UpdatedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Ok(true, "Product updated successfully");
    }
}
