using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models;
using SurveyPortal.Infrastructure; 

namespace SurveyPortal.Controllers
{
    public class SurveySessionController : Controller
    {
        private ISurveyRepository repository;
        private SurveySession surveySession;

        public SurveySessionController(ISurveyRepository repo, SurveySession session)
        {
            repository = repo;
            surveySession = session;
        }

        [HttpPost]
        public IActionResult AddToSession(long surveyId, string returnUrl)
        {
            Survey? survey = repository.Surveys
                .FirstOrDefault(s => s.SurveyID == surveyId);

            if (survey != null)
            {
                surveySession.AddSurvey(survey);
            }
            return Redirect(returnUrl ?? "/");
        }
        [HttpPost]
        public IActionResult RemoveFromSession(long surveyId, string returnUrl)
        {
            Survey? survey = repository.Surveys
                .FirstOrDefault(s => s.SurveyID == surveyId);

            if (survey != null)
            {
                surveySession.RemoveSurvey(survey);
            }
            return Redirect(returnUrl ?? "/");
        }

        // Сторінка для перегляду незавершених опитувань
        public IActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl ?? "/";
            return View(surveySession);
        }
    }
}