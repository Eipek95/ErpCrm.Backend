using ErpCrm.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ErpCrm.Infrastructure.CurrentUser;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            return int.TryParse(userId, out var id)
                ? id
                : null;
        }
    }

    public string? Email =>
        _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.Email)?
            .Value;

    public string? FullName =>
        _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.Name)?
            .Value;

    public IReadOnlyList<string> Roles =>
        _httpContextAccessor.HttpContext?
            .User?
            .FindAll(ClaimTypes.Role)
            .Select(x => x.Value)
            .ToList()
        ?? new List<string>();

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?
            .User?
            .Identity?
            .IsAuthenticated ?? false;
}