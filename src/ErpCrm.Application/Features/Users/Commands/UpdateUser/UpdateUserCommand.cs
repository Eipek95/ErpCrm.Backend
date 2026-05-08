using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}
