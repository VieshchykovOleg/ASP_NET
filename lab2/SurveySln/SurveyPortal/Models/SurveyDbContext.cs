using Microsoft.EntityFrameworkCore;

namespace SurveyPortal.Models
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options) { }
        // DbSet ��� ������ � �������� ��������� (Surveys)
        public DbSet<Survey> Surveys => Set<Survey>();
    }
}