using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = null!;
}
