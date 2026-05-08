using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Enums;
using ErpCrm.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Payments.Commands.CompletePayment;

public class CompletePaymentCommandHandler
    : IRequestHandler<CompletePaymentCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public CompletePaymentCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(
        CompletePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(x => x.Id == request.PaymentId, cancellationToken);

        if (payment is null)
            return Result<bool>.NotFound("Payment not found");

        if (payment.Status == PaymentStatus.Paid)
            return Result<bool>.Fail("Payment already completed");

        payment.Status = PaymentStatus.Paid;
        payment.PaidDate = DateTime.UtcNow;
        payment.UpdatedDate = DateTime.UtcNow;

        payment.AddDomainEvent(new PaymentCompletedEvent(
            payment.Id,
            payment.OrderId,
            request.UserId,
            payment.Amount));

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Payment completed successfully");
    }
}