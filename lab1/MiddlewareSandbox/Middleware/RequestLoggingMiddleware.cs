// Middleware ��� ��������� ������
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // ������ ����� �� ���� ������
        var method = context.Request.Method;
        var path = context.Request.Path;
        var queryString = context.Request.QueryString.HasValue
            ? context.Request.QueryString.Value
            : string.Empty;

        _logger.LogInformation("Request: {Method} {Path}{QueryString}",
            method, path, queryString);

        // ����� �������� � ������� ��� ��������
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Request: {method} {path}{queryString}");

        // �������� ����� ��� �� pipeline
        await _next(context);

        // ����� ����� �������� ������ ��� ������
        _logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Response: {context.Response.StatusCode}");
    }
}

// Extension method ��� �������� ��������� middleware
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}