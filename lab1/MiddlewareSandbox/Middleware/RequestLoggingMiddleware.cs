// Middleware для логування запитів
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
        // Логуємо метод та шлях запиту
        var method = context.Request.Method;
        var path = context.Request.Path;
        var queryString = context.Request.QueryString.HasValue
            ? context.Request.QueryString.Value
            : string.Empty;

        _logger.LogInformation("Request: {Method} {Path}{QueryString}",
            method, path, queryString);

        // Також виводимо в консоль для наочності
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Request: {method} {path}{queryString}");

        // Передаємо запит далі по pipeline
        await _next(context);

        // Можна також логувати статус код відповіді
        _logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Response: {context.Response.StatusCode}");
    }
}

// Extension method для зручного додавання middleware
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}