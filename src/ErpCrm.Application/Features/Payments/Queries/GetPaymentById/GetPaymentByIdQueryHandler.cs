using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Payments.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Payments.Queries.GetPaymentById;

public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Result<PaymentDto>>
{
    private readonly IAppDbContext _context;

    public GetPaymentByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PaymentDto>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Payments
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new PaymentDto
            {
                Id = x.Id,
                OrderId = x.OrderId,
                OrderNumber = x.Order.OrderNumber,
                Status = (int)x.Status,
                Method = (int)x.Method,
                Amount = x.Amount,
                PaidDate = x.PaidDate,
                CreatedDate = x.CreatedDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        return item is null
            ? Result<PaymentDto>.NotFound("Payment not found")
            : Result<PaymentDto>.Ok(item);
    }
}
