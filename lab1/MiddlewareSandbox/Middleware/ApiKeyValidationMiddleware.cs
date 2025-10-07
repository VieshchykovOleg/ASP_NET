// Middleware для перевірки API ключа
public class ApiKeyValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private const string API_KEY_HEADER = "X-API-KEY";

    public ApiKeyValidationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Отримуємо API ключ з конфігурації (можна задати в appsettings.json)
        var validApiKey = _configuration["ApiKey"] ?? "my-secret-api-key-12345";

        // Перевіряємо наявність заголовка X-API-KEY
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
        {
            // Заголовок відсутній
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\": \"API Key is missing\"}");
            return;
        }

        // Перевіряємо, чи співпадає API ключ
        if (!validApiKey.Equals(extractedApiKey))
        {
            // Ключ неправильний
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\": \"Invalid API Key\"}");
            return;
        }

        // Ключ правильний, передаємо запит далі
        await _next(context);
    }
}

// Extension method для зручного додавання middleware
public static class ApiKeyValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseApiKeyValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiKeyValidationMiddleware>();
    }
}