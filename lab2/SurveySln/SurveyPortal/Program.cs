using Microsoft.EntityFrameworkCore;
using SurveyPortal.Models;
using SurveyPortal.Data.Models;
using Microsoft.AspNetCore.Identity;

// ВАЖЛИВО: Тут НЕМАЄ "using Microsoft.OpenApi.Models;", це для API проекту.

var builder = WebApplication.CreateBuilder(args);

// 1. MVC, DbContext та Репозиторій
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SurveyDbContext>(opts => {
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:SurveyPortalConnection"],
        b => b.MigrationsAssembly("SurveyPortal")
    );
});
builder.Services.AddScoped<ISurveyRepository, EFSurveyRepository>();

// 2. Сервіси Сесій
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
        builder.Configuration["ConnectionStrings:IdentityConnection"],
        b => b.MigrationsAssembly("SurveyPortal")
    )
);

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

// Наповнення БД
SeedData.EnsurePopulated(app);
await IdentitySeedData.EnsurePopulatedAsync(app);

app.Run();