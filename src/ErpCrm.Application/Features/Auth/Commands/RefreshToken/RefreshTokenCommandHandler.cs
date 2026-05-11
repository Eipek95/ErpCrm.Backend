using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Auth.Commands.RefreshToken;
using ErpCrm.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Auth.RefreshToken;

public class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, Result<AuthResponseDto>>
{
    private readonly IAppDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly ICurrentRequestService _currentRequestService;

    public RefreshTokenCommandHandler(
        IAppDbContext context,
        IJwtService jwtService,
        ICurrentRequestService currentRequestService)
    {
        _context = context;
        _jwtService = jwtService;
        _currentRequestService = currentRequestService;
    }

    public async Task<Result<AuthResponseDto>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(x => x.User)
                .ThenInclude(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(
                x => x.Token == request.RefreshToken,
                cancellationToken);

        if (refreshToken is null)
            return Result<AuthResponseDto>.Fail(
                "Invalid refresh token",
                401);

        if (!refreshToken.IsActive)
            return Result<AuthResponseDto>.Fail(
                "Refresh token expired or revoked",
                401);

        var user = refreshToken.User;

        var roles = user.UserRoles
            .Select(x => x.Role.Name)
            .ToList();

        var newAccessToken =
            _jwtService.GenerateAccessToken(user, roles);

        var newRefreshToken =
            _jwtService.GenerateRefreshToken();

        var accessTokenExpiresAt =
            _jwtService.GetAccessTokenExpiryDate();

        var refreshTokenExpiresAt =
            _jwtService.GetRefreshTokenExpiryDate();

        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.RevokedByIp =
            _currentRequestService.GetIpAddress();

        refreshToken.ReplacedByToken = newRefreshToken;

        var newRefreshTokenEntity = new Domain.Entities.RefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiresAt = refreshTokenExpiresAt,
            CreatedByIp =
                _currentRequestService.GetIpAddress(),
            CreatedDate = DateTime.UtcNow
        };

        await _context.RefreshTokens.AddAsync(
            newRefreshTokenEntity,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<AuthResponseDto>.Ok(
            new AuthResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Roles = roles,

                AccessToken = newAccessToken,
                AccessTokenExpiresAt = accessTokenExpiresAt,

                RefreshToken = newRefreshToken,
                RefreshTokenExpiresAt = refreshTokenExpiresAt
            });
    }
}