using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Payments.Commands.UpdatePayment;

public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public UpdatePaymentCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Payments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            return Result<bool>.NotFound("Payment not found");

        if (!Enum.IsDefined(typeof(PaymentStatus), request.Status))
            return Result<bool>.Fail("Invalid payment status");

        if (!Enum.IsDefined(typeof(PaymentMethod), request.Method))
            return Result<bool>.Fail("Invalid payment method");

        entity.Status = (PaymentStatus)request.Status;
        entity.Method = (PaymentMethod)request.Method;
        entity.Amount = request.Amount;
        entity.PaidDate = request.PaidDate;
        entity.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Payment updated successfully");
    }
}
