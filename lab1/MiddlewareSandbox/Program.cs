var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Реєструємо сервіс для підрахунку запитів
builder.Services.AddSingleton<RequestCounterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestCounter();
app.UseRequestLogging();
app.UseCustomParameterCheck();  // ⬅️ Переміщено вище
app.UseApiKeyValidation();      // ⬅️ Тепер нижче
app.UseAuthorization();
app.MapControllers();

app.Run();