using Microsoft.EntityFrameworkCore; // <--- ОСЬ ЦЕ ВИРІШУЄ ПОМИЛКУ 1
using SurveyPortal.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Налаштування сервісів для MVC
builder.Services.AddControllersWithViews();

// 2. DbContext
builder.Services.AddDbContext<SurveyDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:SurveyPortalConnection"]);
});

// 3. Репозиторій
builder.Services.AddScoped<ISurveyRepository, EFSurveyRepository>();

// 4. Сервіси сесій (для Завдання 3)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<SurveySession>(sp => SessionSurveySession.GetSession(sp));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// --- Налаштування Pipeline ---

// Дозвіл на обслуговування статичних файлів (з wwwroot)
app.UseStaticFiles();

// Активація сесій (до маршрутизації)
app.UseSession();

// Реєстрація стандартного маршруту MVC
app.MapDefaultControllerRoute();

// Наповнення БД
SeedData.EnsurePopulated(app);

app.Run();

