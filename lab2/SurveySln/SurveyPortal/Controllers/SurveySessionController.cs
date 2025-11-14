using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models;
using SurveyPortal.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace SurveyPortal.Controllers
{
    [Authorize]
    public class SurveySessionController : Controller
    {
        private ISurveyRepository repository;
        private SurveySession surveySession;

        public SurveySessionController(ISurveyRepository repo, SurveySession session)
        {
            repository = repo;
            surveySession = session;
        }

        // --- ADD TO SESSION ---
        [HttpPost]
        public IActionResult AddToSession(long surveyId, string returnUrl)
        {
            // 1. Спочатку знаходимо об'єкт Survey
            Survey? survey = repository.Surveys
                .FirstOrDefault(s => s.SurveyID == surveyId);

            if (survey != null)
            {
                // 2. Потім передаємо ОБ'ЄКТ у сесію
                surveySession.AddSurvey(survey);
            }
            return Redirect(returnUrl ?? "/");
        }

        // --- REMOVE FROM SESSION (тут була твоя помилка, рядок ~43) ---
        [HttpPost]
        public IActionResult RemoveFromSession(long surveyId, string returnUrl)
        {
            // 1. Спочатку знаходимо об'єкт Survey
            Survey? survey = repository.Surveys
                .FirstOrDefault(s => s.SurveyID == surveyId);

            if (survey != null)
            {
                // 2. Потім передаємо ОБ'ЄКТ для видалення
                surveySession.RemoveSurvey(survey);
            }
            return Redirect(returnUrl ?? "/");
        }

        // --- INDEX (Сторінка кошика) ---
        public IActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl ?? "/";
            return View(surveySession);
        }
    }
}