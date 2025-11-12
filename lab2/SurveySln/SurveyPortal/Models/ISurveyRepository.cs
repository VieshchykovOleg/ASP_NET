namespace SurveyPortal.Models
{
    public interface ISurveyRepository
    {
        IQueryable<Survey> Surveys { get; }

        void SaveSurvey(Survey survey);
        Survey? DeleteSurvey(long surveyID);
    }
}