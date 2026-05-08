using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Dashboard.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Dashboard.Queries.GetMonthlySales;

public class GetMonthlySalesQuery : IRequest<Result<List<MonthlySalesDto>>>
{
    public int? Year { get; set; }
}