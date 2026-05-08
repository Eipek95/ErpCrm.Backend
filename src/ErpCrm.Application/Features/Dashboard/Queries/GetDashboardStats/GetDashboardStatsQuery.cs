using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Dashboard.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Dashboard.Queries.GetDashboardStats;

public class GetDashboardStatsQuery : IRequest<Result<DashboardStatsDto>>
{
}