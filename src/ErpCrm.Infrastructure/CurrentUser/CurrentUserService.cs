using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace ErpCrm.Infrastructure.CurrentUser;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private CurrentUserModel? CurrentUser =>
        _httpContextAccessor.HttpContext?.Items["CurrentUser"] as CurrentUserModel;

    public int? UserId => CurrentUser?.UserId;

    public string? Email => CurrentUser?.Email;

    public string? FullName => CurrentUser?.FullName;

    public IReadOnlyList<string> Roles =>
        CurrentUser?.Roles ?? new List<string>();

    public bool IsAuthenticated =>
        CurrentUser is not null;
}