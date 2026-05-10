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

    public static IApplicationBuilder UseRequestTimingMiddleware(
    this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestTimingMiddleware>();
    }

    public static IApplicationBuilder UseRequestLoggingMiddleware(
    this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestLoggingMiddleware>();
    }
}