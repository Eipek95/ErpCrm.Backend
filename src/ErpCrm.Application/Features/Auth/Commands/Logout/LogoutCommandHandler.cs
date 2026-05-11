using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Auth.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result<bool>>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentRequestService _currentRequestService;

    public LogoutCommandHandler(
        IAppDbContext context,
        ICurrentRequestService currentRequestService)
    {
        _context = context;
        _currentRequestService = currentRequestService;
    }

    public async Task<Result<bool>> Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(
                x => x.Token == request.RefreshToken,
                cancellationToken);

        if (refreshToken is null)
            return Result<bool>.Fail("Invalid refresh token", 401);

        if (refreshToken.IsRevoked)
            return Result<bool>.Ok(true, "Token already revoked");

        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.RevokedByIp = _currentRequestService.GetIpAddress();
        refreshToken.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Logout successful");
    }
}