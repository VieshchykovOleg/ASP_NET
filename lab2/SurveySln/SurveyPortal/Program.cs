using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SurveyPortal.Models;

var builder = WebApplication.CreateBuilder(args);

// Додавання служб MVC
builder.Services.AddControllersWithViews();

// Додавання підтримки сесій
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// ✅ Реєстрація контексту бази даних
builder.Services.AddDbContext<SurveyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SurveyConnection")));

// ✅ Реєстрація репозиторію
builder.Services.AddScoped<ISurveyRepository, EFSurveyRepository>();

var app = builder.Build();

SeedData.EnsurePopulated(app);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Використання сесій перед маршрутизацією
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
