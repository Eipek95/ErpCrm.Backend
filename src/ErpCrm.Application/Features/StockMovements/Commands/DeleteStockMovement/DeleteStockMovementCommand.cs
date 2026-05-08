using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.StockMovements.Commands.DeleteStockMovement;

public record DeleteStockMovementCommand(int Id) : IRequest<Result<bool>>;
