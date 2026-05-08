using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.StockMovements.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.StockMovements.Queries.GetStockMovementById;

public record GetStockMovementByIdQuery(int Id) : IRequest<Result<StockMovementDto>>;
