using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Warehouses.Commands.DeleteWarehouse;
public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, Result<bool>>
{
    private readonly IAppDbContext _context;
    public DeleteWarehouseCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<bool>> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null) return Result<bool>.NotFound("Warehouse not found");
        entity.IsDeleted = true; entity.DeletedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Ok(true, "Warehouse deleted successfully");
    }
}
