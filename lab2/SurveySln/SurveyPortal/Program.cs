using Microsoft.EntityFrameworkCore;
using SurveyPortal.Models; // <-- ОСЬ ЦЕЙ РЯДОК ВИПРАВЛЯЄ ПОМИЛКУ
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// 1. MVC, DbContext та Репозиторій (з минулих лаб)
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SurveyDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:SurveyPortalConnection"]);
});
builder.Services.AddScoped<ISurveyRepository, EFSurveyRepository>();

// 2. Сервіси Сесій (з минулих лаб)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Цей рядок (рядок 17) тепер буде працювати
builder.Services.AddScoped<SurveySession>(sp => SessionSurveySession.GetSession(sp));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 3. Конфігурація Identity
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:IdentityConnection"])
);

// Додаємо Identity з підтримкою РОЛЕЙ
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 8;
    opts.Password.RequireDigit = true;
    opts.Password.RequireUppercase = true;
    opts.Password.RequireNonAlphanumeric = false;
    opts.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);
// Створення ролей та Адміна
await IdentitySeedData.EnsurePopulatedAsync(app);

app.Run();