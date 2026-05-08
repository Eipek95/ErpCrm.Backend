using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.Warehouses.Commands.UpdateWarehouse;
public class UpdateWarehouseCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
}
