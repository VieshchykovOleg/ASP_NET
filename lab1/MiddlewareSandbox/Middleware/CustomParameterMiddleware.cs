// Middleware ��� �������� ��������� "custom" � query string
public class CustomParameterMiddleware
{
    private readonly RequestDelegate _next;

    public CustomParameterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // ���������� �������� ��������� "custom" � query string
        if (context.Request.Query.ContainsKey("custom"))
        {
            // ���� �������� ��������, ��������� ������� � ��������� pipeline
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("You've hit a custom middleware!");
            return; // �� ��������� _next(), ��������� �������
        }

        // ���� ��������� ����, �������� ����� ���
        await _next(context);
    }
}

// Extension method ��� �������� ��������� middleware
public static class CustomParameterMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomParameterCheck(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomParameterMiddleware>();
    }
}