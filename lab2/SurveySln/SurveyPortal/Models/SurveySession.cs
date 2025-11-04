using System.Text.Json.Serialization;
using SurveyPortal.Infrastructure; // Потрібен для SessionExtensions

namespace SurveyPortal.Models
{
    // Окремий клас для рядка "кошика"
    public class SurveySessionLine
    {
        public long SurveyID { get; set; }
        public string Title { get; set; } = string.Empty;
    }

    // Клас самого "кошика"
    public class SurveySession
    {
        public List<SurveySessionLine> Lines { get; set; } = new List<SurveySessionLine>();

        public virtual void AddSurvey(Survey survey)
        {
            SurveySessionLine? line = Lines
                .Where(s => s.SurveyID == survey.SurveyID)
                .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new SurveySessionLine
                {
                    // ВИРІШЕННЯ ПОМИЛКИ CS0266:
                    // Ми додаємо явний каст (long), 
                    // оскільки survey.SurveyID є long?, а SurveyID - long.
                    SurveyID = (long)survey.SurveyID!,
                    Title = survey.Title
                });
            }
        }

        public virtual void RemoveSurvey(Survey survey) =>
            Lines.RemoveAll(l => l.SurveyID == survey.SurveyID);

        public virtual void Clear() => Lines.Clear();
    }


    // Клас для роботи з сесією
    public class SessionSurveySession : SurveySession
    {
        private const string SessionKey = "SurveySession";

        [JsonIgnore]
        public ISession? Session { get; private set; }

        public static SurveySession GetSession(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?.Session;

            SessionSurveySession surveySession = session?.GetJson<SessionSurveySession>(SessionKey)
                ?? new SessionSurveySession();

            surveySession.Session = session;
            return surveySession;
        }

        public override void AddSurvey(Survey survey)
        {
            base.AddSurvey(survey);
            Session?.SetJson(SessionKey, this);
        }

        public override void RemoveSurvey(Survey survey)
        {
            base.RemoveSurvey(survey);
            Session?.SetJson(SessionKey, this);
        }

        public override void Clear()
        {
            base.Clear();
            Session?.SetJson(SessionKey, this);
        }
    }
}

