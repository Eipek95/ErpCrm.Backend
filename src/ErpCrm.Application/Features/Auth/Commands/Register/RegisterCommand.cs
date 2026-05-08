using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Auth.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<Result<AuthResponseDto>>
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}