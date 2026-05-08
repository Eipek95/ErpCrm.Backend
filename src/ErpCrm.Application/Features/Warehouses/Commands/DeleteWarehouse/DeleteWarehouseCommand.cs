using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.Warehouses.Commands.DeleteWarehouse;
public record DeleteWarehouseCommand(int Id) : IRequest<Result<bool>>;
