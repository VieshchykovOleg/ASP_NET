using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // Додайте цей using

namespace SurveyPortal.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            // Використання IApplicationBuilder для отримання DbContext
            using (var scope = app.ApplicationServices.CreateScope())
            {
                SurveyDbContext context = scope.ServiceProvider.GetRequiredService<SurveyDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate(); // Застосування міграцій
                }

                if (!context.Surveys.Any())
                {
                    context.Surveys.AddRange(
                    new Survey
                    {
                        Title = "Опитування: Задоволеність сервісом",
                        Description = "Збір відгуків про якість надання послуг.",
                        Creator = "Адміністратор 1",
                        AverageRating = 4.5m
                    },
                    new Survey
                    {
                        Title = "Опитування: Плани на відпустку",
                        Description = "Дізнайтеся, куди планують поїхати наші співробітники цього року.",
                        Creator = "HR-відділ",
                        AverageRating = 3.8m
                    },
                    new Survey
                    {
                        Title = "Опитування: Улюблені страви",
                        Description = "Визначення найпопулярніших страв серед користувачів.",
                        Creator = "Кулінарний блог",
                        AverageRating = 4.9m
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}

