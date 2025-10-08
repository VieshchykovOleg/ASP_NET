using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

int requestCount = 0;
string validApiKey = "12345";

// Middleware логування
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
});

// Middleware перевірки API-ключа
app.Use(async (context, next) =>
{
    if (!context.Request.Headers.TryGetValue("X-API-KEY", out var apiKey) || apiKey != validApiKey)
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Forbidden: Invalid or missing API key.");
        return;
    }
    await next.Invoke();
});

// Middleware аналізу query string
app.Use(async (context, next) =>
{
    if (context.Request.Query.ContainsKey("custom"))
    {
        await context.Response.WriteAsync("You've hit a custom middleware!");
        return;
    }
    await next.Invoke();
});

// Middleware підрахунку запитів
app.Use(async (context, next) =>
{
    requestCount++;
    await next.Invoke();
    await context.Response.WriteAsync($"\nThe amount of processed requests is {requestCount}.");
});

app.MapGet("/", () => "Hello from Middleware Sandbox!");

app.Run();
