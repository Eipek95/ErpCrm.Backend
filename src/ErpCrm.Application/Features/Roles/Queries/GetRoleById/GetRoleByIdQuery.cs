using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Roles.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Roles.Queries.GetRoleById;

public record GetRoleByIdQuery(int Id) : IRequest<Result<RoleDto>>;
