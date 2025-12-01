using Microsoft.EntityFrameworkCore;
using SurveyPortal.Data.Models; 
using SurveyPortal.Shared;     
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Додавання сервісів контролерів З НАЛАШТУВАННЯМ JSON
// Це дозволяє уникнути помилки "Object cycle detected" при серіалізації пов'язаних даних
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();

// 2. Налаштування Swagger для підтримки Bearer токенів
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SurveyPortal API", Version = "v1" });

    // Визначаємо схему безпеки (Bearer)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Введіть токен у форматі: Bearer {ваш_токен}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Додаємо вимогу безпеки до всіх ендпоінтів
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// 3. Налаштування CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 4. Реєстрація DbContext
builder.Services.AddDbContext<SurveyDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:SurveyPortalConnection"]);
});
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:IdentityConnection"])
);

// 5. Реєстрація репозиторію
builder.Services.AddScoped<ISurveyRepository, EFSurveyRepository>();

// 6. Налаштування Identity та Автентифікації
// ВАЖЛИВО: Вказуємо схему автентифікації та додаємо обробник BearerToken
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
    options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
    options.DefaultSignInScheme = IdentityConstants.BearerScheme; // Виправляє помилку 500 при логіні
})
.AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorization();

builder.Services.AddIdentityCore<IdentityUser>(opts =>
{
    opts.Password.RequiredLength = 8;
    opts.Password.RequireDigit = true;
    opts.Password.RequireUppercase = true;
    opts.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddApiEndpoints(); // Цей метод також налаштовує генерацію токенів

var app = builder.Build();

// 7. Налаштування Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Важливо: CORS має бути перед Auth
app.UseCors("AllowBlazor");

app.UseAuthentication();
app.UseAuthorization();

// 8. Реєстрація ендпоїнтів
app.MapIdentityApi<IdentityUser>(); // /register, /login
app.MapControllers();

app.Run();