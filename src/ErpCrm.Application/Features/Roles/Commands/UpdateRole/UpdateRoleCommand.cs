using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
