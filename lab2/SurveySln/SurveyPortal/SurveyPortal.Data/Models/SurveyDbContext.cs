using Microsoft.EntityFrameworkCore;
using SurveyPortal.Shared;

namespace SurveyPortal.Shared
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
            : base(options) { }

        public DbSet<Survey> Surveys => Set<Survey>();

        public DbSet<Question> Questions => Set<Question>();
    }
}