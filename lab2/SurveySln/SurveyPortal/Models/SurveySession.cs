using System.Collections.Generic;
using System.Linq;

namespace SurveyPortal.Models
{
    // Модель для збереження незавершених опитувань у сесії
    public class SurveySession
    {
        public List<Survey> Lines { get; set; } = new List<Survey>();

        public void AddSurvey(Survey survey)
        {
            if (!Lines.Any(s => s.SurveyID == survey.SurveyID))
            {
                Lines.Add(survey);
            }
        }

        public void RemoveSurvey(int surveyId)
        {
            Lines.RemoveAll(s => s.SurveyID == surveyId);
        }

        public void Clear() => Lines.Clear();
    }
}
