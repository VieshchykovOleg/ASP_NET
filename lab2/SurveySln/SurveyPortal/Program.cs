using Microsoft.EntityFrameworkCore;
using SurveyPortal.Models;
var builder = WebApplication.CreateBuilder(args);

// 1. Налаштування сервісів для MVC
builder.Services.AddControllersWithViews();

// Додавання DbContext та конфігурація підключення
builder.Services.AddDbContext<SurveyDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:SurveyPortalConnection"]);
});

// Реєстрація репозиторію з областю видимості (Scoped)
builder.Services.AddScoped<ISurveyRepository, EFSurveyRepository>();

var app = builder.Build();


// 2. Дозвіл на обслуговування статичних файлів (з wwwroot)
app.UseStaticFiles();

// 3. Реєстрація стандартного маршруту MVC
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
