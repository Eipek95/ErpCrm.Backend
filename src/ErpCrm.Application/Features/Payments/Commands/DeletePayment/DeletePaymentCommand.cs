using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Payments.Commands.DeletePayment;

public record DeletePaymentCommand(int Id) : IRequest<Result<bool>>;
