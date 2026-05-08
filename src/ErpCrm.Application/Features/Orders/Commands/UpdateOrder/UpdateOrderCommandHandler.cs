using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public UpdateOrderCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (order is null)
            return Result<bool>.NotFound("Order not found");

        if (!Enum.IsDefined(typeof(OrderStatus), request.Status))
            return Result<bool>.Fail("Invalid order status");

        order.Status = (OrderStatus)request.Status;
        order.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Order updated successfully");
    }
}
