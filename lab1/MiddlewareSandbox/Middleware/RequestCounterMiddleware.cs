// Middleware ��� ��������� ������
public class RequestCounterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RequestCounterService _counterService;

    public RequestCounterMiddleware(RequestDelegate next, RequestCounterService counterService)
    {
        _next = next;
        _counterService = counterService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // �������� ��������
        _counterService.IncrementCounter();

        // ������ ��������� � ������� ������
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Add("X-Request-Count",
                $"The amount of processed requests is {_counterService.GetCount()}");
            return Task.CompletedTask;
        });

        // �������� ����� ��� �� pipeline
        await _next(context);
    }
}

// ����� ��� ���������� ����� ���������
public class RequestCounterService
{
    private int _requestCount = 0;
    private readonly object _lock = new object();

    public void IncrementCounter()
    {
        lock (_lock)
        {
            _requestCount++;
        }
    }

    public int GetCount()
    {
        lock (_lock)
        {
            return _requestCount;
        }
    }
}

// Extension method ��� �������� ��������� middleware
public static class RequestCounterMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestCounter(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestCounterMiddleware>();
    }
}