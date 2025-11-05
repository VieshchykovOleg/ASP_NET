using Microsoft.EntityFrameworkCore;

namespace SurveyPortal.Models
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
            : base(options) { }

        public DbSet<Survey> Surveys { get; set; }
    }
}
