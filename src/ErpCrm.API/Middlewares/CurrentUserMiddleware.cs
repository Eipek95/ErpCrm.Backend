using System.Security.Claims;
using ErpCrm.Application.Common.Models;

namespace ErpCrm.API.Middlewares;

public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdValue = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdValue, out var userId))
            {
                var currentUser = new CurrentUserModel
                {
                    UserId = userId,
                    Email = context.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
                    FullName = context.User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                    Roles = context.User
                        .FindAll(ClaimTypes.Role)
                        .Select(x => x.Value)
                        .ToList()
                };

                context.Items["CurrentUser"] = currentUser;
            }
        }

        await _next(context);
    }
}