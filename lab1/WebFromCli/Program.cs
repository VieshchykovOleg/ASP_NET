var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/who", () => "���� º�����");
app.MapGet("/time", () => DateTime.Now.ToString("HH:mm:ss"));

app.Run();
