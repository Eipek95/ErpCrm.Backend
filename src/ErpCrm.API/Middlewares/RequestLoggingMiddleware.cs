using System.Diagnostics;
using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Entities;
using Serilog;

namespace ErpCrm.API.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ICurrentUserService currentUserService,
        ICurrentRequestService currentRequestService,
        IAppDbContext dbContext)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);

            stopwatch.Stop();

            await SaveRequestLogAsync(
                context,
                currentUserService,
                currentRequestService,
                dbContext,
                stopwatch.ElapsedMilliseconds);

            Log.Information(
                "HTTP {Method} {Path} responded {StatusCode} in {Elapsed} ms | UserId: {UserId}",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds,
                currentUserService.UserId);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            await SaveRequestLogAsync(
                context,
                currentUserService,
                currentRequestService,
                dbContext,
                stopwatch.ElapsedMilliseconds);

            Log.Error(
                ex,
                "HTTP {Method} {Path} failed in {Elapsed} ms | UserId: {UserId}",
                context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds,
                currentUserService.UserId);

            throw;
        }
    }

    private static async Task SaveRequestLogAsync(
        HttpContext context,
        ICurrentUserService currentUserService,
        ICurrentRequestService currentRequestService,
        IAppDbContext dbContext,
        long elapsedMs)
    {
        if (context.Request.Path.StartsWithSegments("/hangfire") ||
            context.Request.Path.StartsWithSegments("/swagger"))
        {
            return;
        }

        await dbContext.RequestLogs.AddAsync(new RequestLog
        {
            UserId = currentUserService.UserId,
            Method = context.Request.Method,
            Path = context.Request.Path,
            StatusCode = context.Response.StatusCode,
            ExecutionTimeMs = elapsedMs,
            IPAddress = currentRequestService.GetIpAddress(),
            UserAgent = currentRequestService.GetUserAgent(),
            CorrelationId = currentRequestService.GetCorrelationId(),
            CreatedDate = DateTime.UtcNow
        });

        await dbContext.SaveChangesAsync();
    }
}