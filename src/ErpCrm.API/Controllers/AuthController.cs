using ErpCrm.Application.Features.Auth.Commands.Login;
using ErpCrm.Application.Features.Auth.Commands.RefreshToken;
using ErpCrm.Application.Features.Auth.Commands.Register;
using ErpCrm.Application.Features.Auth.Logout;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
    [FromBody] LogoutCommand command)
    {
        var result = await _mediator.Send(command);

        return StatusCode(result.StatusCode, result);
    }
}