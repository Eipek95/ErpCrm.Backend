using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Auth.Commands.Logout;

public class LogoutCommandHandler
    : IRequestHandler<LogoutCommand, Result<bool>>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public LogoutCommandHandler(
        IAppDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<bool>> Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated ||
            _currentUserService.UserId is null)
        {
            return Result<bool>.Fail("Unauthorized", 401);
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId.Value,
                cancellationToken);

        if (user is null)
            return Result<bool>.NotFound("User not found");

        user.RefreshToken = null;
        user.RefreshTokenExpiresAt = null;
        user.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Logout successful");
    }
}