using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SurveyPortal.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            SurveyDbContext context = app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<SurveyDbContext>();

            if (!context.Surveys.Any())
            {
                context.Surveys.AddRange(
                    new Survey
                    {
                        Title = "Опитування про спорт",
                        Description = "Ваше ставлення до активного способу життя",
                        Creator = "Admin",
                        AverageRating = 4.2M,
                        Category = "Спорт"
                    },
                    new Survey
                    {
                        Title = "Опитування про політику",
                        Description = "Що ви думаєте про вибори?",
                        Creator = "Admin",
                        AverageRating = 3.8M,
                        Category = "Політика"
                    },
                    new Survey
                    {
                        Title = "Опитування про фільми",
                        Description = "Ваш улюблений жанр кіно?",
                        Creator = "Admin",
                        AverageRating = 4.5M,
                        Category = "Культура"
                    }
                );
                context.SaveChanges();
            }
        }

    }
}

