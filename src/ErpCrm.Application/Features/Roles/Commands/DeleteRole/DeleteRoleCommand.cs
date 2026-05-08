using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Roles.Commands.DeleteRole;

public record DeleteRoleCommand(int Id) : IRequest<Result<bool>>;
