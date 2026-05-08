using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Payments.Commands.DeletePayment;

public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public DeletePaymentCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Payments.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            return Result<bool>.NotFound("Payment not found");

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Payment deleted successfully");
    }
}
