using Microsoft.EntityFrameworkCore;
using SurveyPortal.Data.Models; // Твій новий using
using Microsoft.AspNetCore.Builder; // Потрібен для IApplicationBuilder
using Microsoft.Extensions.DependencyInjection; // Потрібен для CreateScope
using SurveyPortal.Shared;
namespace SurveyPortal.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            // БЛОК using (var scope...) МАЄ БУТИ ТУТ
            using (var scope = app.ApplicationServices.CreateScope())
            {
                // SurveyDbContext тепер з Data.Models
                SurveyDbContext context = scope.ServiceProvider.GetRequiredService<SurveyDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Surveys.Any())
                {
                    // Додай тут свій SeedData
                    context.Surveys.AddRange(
                        new Survey
                        {
                            Title = "Опитування: Задоволеність сервісом",
                            Description = "Збір відгуків про якість надання послуг.",
                            Creator = "Адміністратор 1",
                            AverageRating = 4.50M,
                            Category = "Сервіс"
                        },
                        new Survey
                        {
                            Title = "Опитування: Плани на відпустку",
                            Description = "Дізнайтеся, куди планують поїхати наші співробітники цього року.",
                            Creator = "HR-відділ",
                            AverageRating = 3.80M,
                            Category = "HR"
                        },
                         new Survey
                          {
                              Title = "Опитування: про вас",
                              Description = "Дізнайтеся, про вас більше.",
                              Creator = "HR-відділ",
                              AverageRating = 2.90M,
                              Category = "HR"
                          }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}