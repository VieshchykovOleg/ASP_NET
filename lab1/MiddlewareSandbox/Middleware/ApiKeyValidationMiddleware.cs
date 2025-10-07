// Middleware ��� �������� API �����
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
        // �������� API ���� � ������������ (����� ������ � appsettings.json)
        var validApiKey = _configuration["ApiKey"] ?? "my-secret-api-key-12345";

        // ���������� �������� ��������� X-API-KEY
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
        {
            // ��������� �������
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\": \"API Key is missing\"}");
            return;
        }

        // ����������, �� ������� API ����
        if (!validApiKey.Equals(extractedApiKey))
        {
            // ���� ������������
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\": \"Invalid API Key\"}");
            return;
        }

        // ���� ����������, �������� ����� ���
        await _next(context);
    }
}

// Extension method ��� �������� ��������� middleware
public static class ApiKeyValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseApiKeyValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiKeyValidationMiddleware>();
    }
}