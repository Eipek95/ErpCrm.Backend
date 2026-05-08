using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommand : IRequest<Result<int>>
{
    public int OrderId { get; set; }
    public int Status { get; set; }
    public int Method { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidDate { get; set; } = DateTime.UtcNow;
}
