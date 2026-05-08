using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using ErpCrm.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Result<int>>
{
    private readonly IAppDbContext _context;

    public CreatePaymentCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var orderExists = await _context.Orders.AnyAsync(x => x.Id == request.OrderId, cancellationToken);
        if (!orderExists)
            return Result<int>.Fail("Order not found");

        var paymentExists = await _context.Payments.AnyAsync(x => x.OrderId == request.OrderId, cancellationToken);
        if (paymentExists)
            return Result<int>.Fail("Payment already exists for this order", 409);

        if (!Enum.IsDefined(typeof(PaymentStatus), request.Status))
            return Result<int>.Fail("Invalid payment status");

        if (!Enum.IsDefined(typeof(PaymentMethod), request.Method))
            return Result<int>.Fail("Invalid payment method");

        var entity = new Payment
        {
            OrderId = request.OrderId,
            Status = (PaymentStatus)request.Status,
            Method = (PaymentMethod)request.Method,
            Amount = request.Amount,
            PaidDate = request.PaidDate,
            CreatedDate = DateTime.UtcNow
        };

        await _context.Payments.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Created(entity.Id, "Payment created successfully");
    }
}
