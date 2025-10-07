// Middleware дл€ перев≥рки параметра "custom" в query string
public class CustomParameterMiddleware
{
    private readonly RequestDelegate _next;

    public CustomParameterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // ѕерев≥р€Їмо на€вн≥сть параметра "custom" в query string
        if (context.Request.Query.ContainsKey("custom"))
        {
            // якщо параметр присутн≥й, повертаЇмо в≥дпов≥дь ≥ зупин€Їмо pipeline
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("You've hit a custom middleware!");
            return; // Ќе викликаЇмо _next(), зупин€Їмо обробку
        }

        // якщо параметра немаЇ, передаЇмо запит дал≥
        await _next(context);
    }
}

// Extension method дл€ зручного додаванн€ middleware
public static class CustomParameterMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomParameterCheck(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomParameterMiddleware>();
    }
}