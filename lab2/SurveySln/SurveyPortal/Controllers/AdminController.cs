using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Data.Models;
using SurveyPortal.Shared;      // Важливо для моделі Survey
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR; // <--- ДОДАНО
using SurveyPortal.Hubs;            // <--- ДОДАНО

namespace SurveyPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ISurveyRepository repository;
        private IHubContext<SurveyHub> hubContext; // <--- 1. Поле для хабу

        // 2. Ін'єкція хабу через конструктор
        public AdminController(ISurveyRepository repo, IHubContext<SurveyHub> hub)
        {
            repository = repo;
            hubContext = hub;
        }

        public IActionResult Index() => View(repository.Surveys);

        public IActionResult Edit(long id)
        {
            Survey? survey = repository.Surveys.FirstOrDefault(s => s.SurveyID == id);
            if (survey == null) return NotFound();
            return View(survey);
        }

        // 3. Метод став асинхронним (async Task)
        [HttpPost]
        public async Task<IActionResult> Edit(Survey survey)
        {
            if (ModelState.IsValid)
            {
                repository.SaveSurvey(survey);
                TempData["message"] = $"Опитування '{survey.Title}' було збережено.";

                // 4. ВІДПРАВКА ПОВІДОМЛЕННЯ "ReceiveSurveyUpdate"
                // Ми надсилаємо ID, нову назву та новий рейтинг усім підключеним клієнтам.
                await hubContext.Clients.All.SendAsync("ReceiveSurveyUpdate",
                    survey.SurveyID, survey.Title, survey.AverageRating);

                return RedirectToAction("Index");
            }
            else
            {
                return View(survey);
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(long surveyID)
        {
            repository.DeleteSurvey(surveyID);
            return RedirectToAction("Index");
        }
    }
}