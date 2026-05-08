using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Roles.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Roles.Queries.GetRoles;

public class GetRolesQuery : PagedRequest, IRequest<Result<PagedResult<RoleDto>>>
{
}
