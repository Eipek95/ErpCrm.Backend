using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Auth.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<Result<AuthResponseDto>>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}