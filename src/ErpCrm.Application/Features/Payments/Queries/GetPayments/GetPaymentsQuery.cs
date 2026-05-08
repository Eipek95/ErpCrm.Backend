using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Payments.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Payments.Queries.GetPayments;

public class GetPaymentsQuery : PagedRequest, IRequest<Result<PagedResult<PaymentDto>>>
{
    public int? OrderId { get; set; }
    public int? Status { get; set; }
    public int? Method { get; set; }
}
