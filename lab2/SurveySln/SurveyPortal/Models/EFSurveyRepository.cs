namespace SurveyPortal.Models
{
    public class EFSurveyRepository : ISurveyRepository
    {
        private SurveyDbContext context;

        public EFSurveyRepository(SurveyDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Survey> Surveys => context.Surveys;
    }
}