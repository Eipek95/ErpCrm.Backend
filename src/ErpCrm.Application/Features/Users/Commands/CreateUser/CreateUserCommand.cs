using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<Result<int>>
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}
