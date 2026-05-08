using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.Warehouses.Commands.CreateWarehouse;
public class CreateWarehouseCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
}
