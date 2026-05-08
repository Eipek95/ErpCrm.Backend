using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Payments.Commands.UpdatePayment;

public class UpdatePaymentCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public int Status { get; set; }
    public int Method { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidDate { get; set; }
}
