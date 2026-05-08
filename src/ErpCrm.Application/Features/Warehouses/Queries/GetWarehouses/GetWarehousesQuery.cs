using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Warehouses.DTOs;
using MediatR;
namespace ErpCrm.Application.Features.Warehouses.Queries.GetWarehouses;
public class GetWarehousesQuery : PagedRequest, IRequest<Result<PagedResult<WarehouseDto>>>
{
    public bool? IsActive { get; set; }
    public string? City { get; set; }
}
