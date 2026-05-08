using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, Result<AuthResponseDto>>
{
    private readonly IAppDbContext _context;
    private readonly IJwtService _jwtService;

    public RefreshTokenCommandHandler(
        IAppDbContext context,
        IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<Result<AuthResponseDto>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x =>
                x.RefreshToken == request.RefreshToken,
                cancellationToken);

        if (user is null)
            return Result<AuthResponseDto>.Fail("Invalid refresh token", 401);

        if (!user.IsActive)
            return Result<AuthResponseDto>.Fail("User is passive", 403);

        if (user.RefreshTokenExpiresAt is null ||
            user.RefreshTokenExpiresAt < DateTime.UtcNow)
        {
            return Result<AuthResponseDto>.Fail("Refresh token expired", 401);
        }

        var roles = user.UserRoles
            .Select(x => x.Role.Name)
            .ToList();

        var newAccessToken = _jwtService.GenerateAccessToken(user, roles);
        var newRefreshToken = _jwtService.GenerateRefreshToken();
        var newRefreshTokenExpiresAt = _jwtService.GetRefreshTokenExpiryDate();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiresAt = newRefreshTokenExpiresAt;
        user.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        var response = new AuthResponseDto
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Roles = roles,
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            RefreshTokenExpiresAt = newRefreshTokenExpiresAt
        };

        return Result<AuthResponseDto>.Ok(response, "Token refreshed successfully");
    }
}