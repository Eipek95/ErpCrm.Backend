using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public int Status { get; set; }
}
