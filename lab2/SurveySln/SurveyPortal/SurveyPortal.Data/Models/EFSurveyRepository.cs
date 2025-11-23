namespace SurveyPortal.Data.Models
{
    public class EFSurveyRepository : ISurveyRepository
    {
        private SurveyDbContext context;

        public EFSurveyRepository(SurveyDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Survey> Surveys => context.Surveys;

        // ÐÅÀË²ÇÀÖ²ß CRUD (Ç ÇÀÂÄÀÍÍß 2):

        public void SaveSurvey(Survey survey)
        {
            if (survey.SurveyID == 0)
            {
                // 1. Öå íîâå îïèòóâàííÿ (Create)
                context.Surveys.Add(survey);
            }
            else
            {
                // 2. Öå ³ñíóþ÷å îïèòóâàííÿ (Update)
                Survey? dbEntry = context.Surveys
                    .FirstOrDefault(s => s.SurveyID == survey.SurveyID);
                if (dbEntry != null)
                {
                    dbEntry.Title = survey.Title;
                    dbEntry.Description = survey.Description;
                    dbEntry.Creator = survey.Creator;
                    dbEntry.Category = survey.Category;
                    dbEntry.AverageRating = survey.AverageRating;
                }
            }
            context.SaveChanges();
        }

        public Survey? DeleteSurvey(long surveyID)
        {
            Survey? dbEntry = context.Surveys
                .FirstOrDefault(s => s.SurveyID == surveyID);

            if (dbEntry != null)
            {
                context.Surveys.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}