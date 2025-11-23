namespace SurveyPortal.Data.Models
{
    public interface ISurveyRepository
    {
        IQueryable<Survey> Surveys { get; }

        void SaveSurvey(Survey survey);
        Survey? DeleteSurvey(long surveyID);
    }
}