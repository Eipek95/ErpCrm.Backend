using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Warehouses.Commands.UpdateWarehouse;
public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, Result<bool>>
{
    private readonly IAppDbContext _context;
    public UpdateWarehouseCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<bool>> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null) return Result<bool>.NotFound("Warehouse not found");
        entity.Name = request.Name; entity.City = request.City; entity.Address = request.Address; entity.IsActive = request.IsActive;
        entity.UpdatedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Ok(true, "Warehouse updated successfully");
    }
}
