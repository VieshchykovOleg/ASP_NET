using Microsoft.EntityFrameworkCore;
using SurveyPortal.Models;
var builder = WebApplication.CreateBuilder(args);

// 1. ������������ ������ ��� MVC
builder.Services.AddControllersWithViews();

// ��������� DbContext �� ������������ ����������
builder.Services.AddDbContext<SurveyDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:SurveyPortalConnection"]);
});

// ��������� ���������� � ������� �������� (Scoped)
builder.Services.AddScoped<ISurveyRepository, EFSurveyRepository>();

var app = builder.Build();


// 2. ����� �� �������������� ��������� ����� (� wwwroot)
app.UseStaticFiles();

// 3. ��������� ������������ �������� MVC
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
