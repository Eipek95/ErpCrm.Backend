using System.Net;
using System.Text.Json;
using ErpCrm.Application.Common.Responses;

namespace ErpCrm.API.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var traceId = context.TraceIdentifier;

        _logger.LogError(
            exception,
            "Unhandled exception occurred. TraceId: {TraceId}",
            traceId);


        if (exception is ErpCrm.Application.Common.Exceptions.ValidationException validationException)
        {
            var validationResponse = new ApiErrorResponse
            {
                Success = false,
                Message = "Validation error",
                StatusCode = StatusCodes.Status400BadRequest,
                TraceId = traceId,
                Errors = validationException.Errors
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = validationResponse.StatusCode;

            var validationJson = JsonSerializer.Serialize(validationResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(validationJson);
            return;
        }

        var response = new ApiErrorResponse
        {
            Success = false,
            Message = "An unexpected error occurred.",
            StatusCode = (int)HttpStatusCode.InternalServerError,
            TraceId = traceId,
            Errors = new List<string>
            {
                exception.Message
            }
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}