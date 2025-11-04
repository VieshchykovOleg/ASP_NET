using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // Додайте цей using
using Microsoft.AspNetCore.Builder; // Потрібен для IApplicationBuilder

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
                    // ПОМИЛКА БУЛА ТУТ. Я ПРИБРАВ ЗАЙВИЙ РЯДОК context.Surveys.AddRange(
                    context.Surveys.AddRange(
                        new Survey
                        {
                            Title = "Опитування: Задоволеність сервісом",
                            Description = "Збір відгуків про якість надання послуг.",
                            Creator = "Адміністратор 1",
                            AverageRating = 4.50M, // ВИПРАВЛЕНО: Додано 'M'
                            Category = "Сервіс"
                        },
                        new Survey
                        {
                            Title = "Опитування: Плани на відпустку",
                            Description = "Дізнайтеся, куди планують поїхати наші співробітники цього року.",
                            Creator = "HR-відділ",
                            AverageRating = 3.80M, // ВИПРАВЛЕНО: Додано 'M'
                            Category = "HR"
                        },
                        new Survey
                        {
                            Title = "Опитування: Улюблені страви",
                            Description = "Визначення найпопулярніших страв серед користувачів.",
                            Creator = "Кулінарний блог",
                            AverageRating = 4.90M, // ВИПРАВЛЕНО: Додано 'M'
                            Category = "Розваги"
                        },
                        // Додай ще кілька опитувань з різними категоріями для тестування
                        new Survey
                        {
                            Title = "Опитування: Робоче середовище",
                            Description = "Оцінка офісних умов.",
                            Creator = "HR-відділ",
                            AverageRating = 4.10M, // ВИПРАВЛЕНО: Додано 'M'
                            Category = "HR"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}

