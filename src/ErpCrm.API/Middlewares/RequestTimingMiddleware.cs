using System.Diagnostics;

namespace ErpCrm.API.Middlewares;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestTimingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        context.Items["ExecutionTimeMs"] = stopwatch.ElapsedMilliseconds;
    }
}