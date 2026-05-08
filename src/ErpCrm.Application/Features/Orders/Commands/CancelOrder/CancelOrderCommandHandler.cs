using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Enums;
using ErpCrm.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public CancelOrderCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

        if (order is null)
            return Result<bool>.NotFound("Order not found");

        if (order.Status == OrderStatus.Cancelled)
            return Result<bool>.Fail("Order already cancelled");

        order.Status = OrderStatus.Cancelled;
        order.UpdatedDate = DateTime.UtcNow;

        order.AddDomainEvent(new OrderCancelledEvent(
            order.Id,
            order.OrderNumber,
            request.UserId));

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Order cancelled successfully");
    }
}