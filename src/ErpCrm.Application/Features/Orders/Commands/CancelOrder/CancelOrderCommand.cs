using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Orders.Commands.CancelOrder;

public class CancelOrderCommand : IRequest<Result<bool>>
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
}