using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using MediatR;
namespace ErpCrm.Application.Features.Warehouses.Commands.CreateWarehouse;
public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Result<int>>
{
    private readonly IAppDbContext _context;
    public CreateWarehouseCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<int>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var entity = new Warehouse { Name = request.Name, City = request.City, Address = request.Address, IsActive = request.IsActive, CreatedDate = DateTime.UtcNow };
        await _context.Warehouses.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result<int>.Created(entity.Id, "Warehouse created successfully");
    }
}
