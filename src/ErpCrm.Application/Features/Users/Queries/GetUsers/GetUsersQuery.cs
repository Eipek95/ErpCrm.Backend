using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Users.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Users.Queries.GetUsers;

public class GetUsersQuery : PagedRequest, IRequest<Result<PagedResult<UserDto>>>
{
}
