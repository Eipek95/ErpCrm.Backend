using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(int Id) : IRequest<Result<bool>>;
