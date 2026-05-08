using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Payments.Commands.CompletePayment;

public class CompletePaymentCommand : IRequest<Result<bool>>
{
    public int PaymentId { get; set; }
    public int UserId { get; set; }
}