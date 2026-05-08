using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Auth.DTOs;
using ErpCrm.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthResponseDto>>
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public RegisterCommandHandler(
        IAppDbContext context,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<Result<AuthResponseDto>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLower();

        var emailExists = await _context.Users
            .AnyAsync(x => x.Email == email, cancellationToken);

        if (emailExists)
            return Result<AuthResponseDto>.Fail("Email already exists", 409);

        var defaultRole = await _context.Roles
            .FirstOrDefaultAsync(x => x.Name == "Employee", cancellationToken);

        if (defaultRole is null)
        {
            defaultRole = new Role
            {
                Name = "Employee",
                CreatedDate = DateTime.UtcNow
            };

            await _context.Roles.AddAsync(defaultRole, cancellationToken);
        }

        var refreshToken = _jwtService.GenerateRefreshToken();
        var refreshTokenExpiresAt = _jwtService.GetRefreshTokenExpiryDate();

        var user = new User
        {
            FullName = request.FullName.Trim(),
            Email = email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            IsActive = true,
            RefreshToken = refreshToken,
            RefreshTokenExpiresAt = refreshTokenExpiresAt,
            CreatedDate = DateTime.UtcNow
        };

        user.UserRoles.Add(new UserRole
        {
            User = user,
            Role = defaultRole
        });

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var roles = new List<string> { defaultRole.Name };
        var accessToken = _jwtService.GenerateAccessToken(user, roles);

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

        return Result<AuthResponseDto>.Created(response, "Register successful");
    }
}