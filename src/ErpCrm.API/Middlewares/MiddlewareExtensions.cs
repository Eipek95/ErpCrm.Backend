namespace ErpCrm.API.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionMiddleware>();
    }

    public static IApplicationBuilder UseCurrentUserMiddleware(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<CurrentUserMiddleware>();
    }
}