namespace ErpCrm.Application.Common.Results;

public class Result<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();
    public int StatusCode { get; set; }
    public string? TraceId { get; set; }
    public T? Data { get; set; }
    public static Result<T> Ok(T data, string message = "Success") => new() { Success = true, Message = message, StatusCode = 200, Data = data };
    public static Result<T> Created(T data, string message = "Created") => new() { Success = true, Message = message, StatusCode = 201, Data = data };
    public static Result<T> Fail(string message, int statusCode = 400) => new() { Success = false, Message = message, StatusCode = statusCode, Errors = new() { message } };
    public static Result<T> NotFound(string message = "Record not found") => new() { Success = false, Message = message, StatusCode = 404, Errors = new() { message } };
}
