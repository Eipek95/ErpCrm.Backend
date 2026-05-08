using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Warehouses.DTOs;
using MediatR;
namespace ErpCrm.Application.Features.Warehouses.Queries.GetWarehouseById;
public record GetWarehouseByIdQuery(int Id) : IRequest<Result<WarehouseDto>>;
