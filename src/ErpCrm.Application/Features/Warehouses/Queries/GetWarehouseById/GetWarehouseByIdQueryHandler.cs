using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Warehouses.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Warehouses.Queries.GetWarehouseById;
public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseByIdQuery, Result<WarehouseDto>>
{
    private readonly IAppDbContext _context;
    public GetWarehouseByIdQueryHandler(IAppDbContext context) => _context = context;
    public async Task<Result<WarehouseDto>> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Warehouses.AsNoTracking().Where(x => x.Id == request.Id).Select(x => new WarehouseDto { Id = x.Id, Name = x.Name, City = x.City, Address = x.Address, IsActive = x.IsActive, CreatedDate = x.CreatedDate }).FirstOrDefaultAsync(cancellationToken);
        return item is null ? Result<WarehouseDto>.NotFound("Warehouse not found") : Result<WarehouseDto>.Ok(item);
    }
}
