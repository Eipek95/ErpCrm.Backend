using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Payments.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Payments.Queries.GetPaymentById;

public record GetPaymentByIdQuery(int Id) : IRequest<Result<PaymentDto>>;
