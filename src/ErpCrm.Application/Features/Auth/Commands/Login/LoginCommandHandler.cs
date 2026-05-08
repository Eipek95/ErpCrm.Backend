using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        IAppDbContext context,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<Result<AuthResponseDto>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLower();

        var user = await _context.Users
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        if (user is null)
            return Result<AuthResponseDto>.Fail("Invalid email or password", 401);

        if (!user.IsActive)
            return Result<AuthResponseDto>.Fail("User is passive", 403);

        var passwordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!passwordValid)
            return Result<AuthResponseDto>.Fail("Invalid email or password", 401);

        var roles = user.UserRoles
            .Select(x => x.Role.Name)
            .ToList();

        var accessToken = _jwtService.GenerateAccessToken(user, roles);
        var refreshToken = _jwtService.GenerateRefreshToken();
        var refreshTokenExpiresAt = _jwtService.GetRefreshTokenExpiryDate();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiresAt = refreshTokenExpiresAt;
        user.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        var response = new AuthResponseDto
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Roles = roles,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiresAt = refreshTokenExpiresAt
        };

        return Result<AuthResponseDto>.Ok(response, "Login successful");
    }
}