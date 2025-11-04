using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models;

namespace SurveyPortal.Components
{
    public class SurveySessionSummaryViewComponent : ViewComponent
    {
        private SurveySession surveySession;

        public SurveySessionSummaryViewComponent(SurveySession session)
        {
            surveySession = session;
        }

        public IViewComponentResult Invoke()
        {
            return View(surveySession);
        }
    }
}