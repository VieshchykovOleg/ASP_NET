using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SurveyPortal.Data.Models;

namespace SurveyPortal.Models
{
    public class SurveyDbContextFactory : IDesignTimeDbContextFactory<SurveyDbContext>
    {
        public SurveyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SurveyDbContext>();

            // ⚙️ Твій connection string (заміни при потребі)
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SurveyPortal;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new SurveyDbContext(optionsBuilder.Options);
        }
    }
}
