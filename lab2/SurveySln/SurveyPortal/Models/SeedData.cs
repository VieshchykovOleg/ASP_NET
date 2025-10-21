using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // ������� ��� using

namespace SurveyPortal.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            // ������������ IApplicationBuilder ��� ��������� DbContext
            using (var scope = app.ApplicationServices.CreateScope())
            {
                SurveyDbContext context = scope.ServiceProvider.GetRequiredService<SurveyDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate(); // ������������ �������
                }

                if (!context.Surveys.Any())
                {
                    context.Surveys.AddRange(
                    new Survey
                    {
                        Title = "����������: ������������ �������",
                        Description = "��� ������ ��� ����� ������� ������.",
                        Creator = "����������� 1",
                        AverageRating = 4.5m
                    },
                    new Survey
                    {
                        Title = "����������: ����� �� ��������",
                        Description = "ĳ��������, ���� �������� ������ ���� ����������� ����� ����.",
                        Creator = "HR-����",
                        AverageRating = 3.8m
                    },
                    new Survey
                    {
                        Title = "����������: ������� ������",
                        Description = "���������� �������������� ����� ����� ������������.",
                        Creator = "��������� ����",
                        AverageRating = 4.9m
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}

