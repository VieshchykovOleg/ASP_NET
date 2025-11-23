using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace SurveyPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ISurveyRepository repository;

        public AdminController(ISurveyRepository repo)
        {
            repository = repo;
        }

        // READ (List)
        public IActionResult Index() => View(repository.Surveys);

        // CREATE / EDIT (GET)
        public IActionResult Edit(long id)
        {
            // Survey тепер відомий
            Survey? survey = repository.Surveys
                .FirstOrDefault(s => s.SurveyID == id);

            if (survey == null)
            {
                return NotFound();
            }
            return View(survey);
        }

        // CREATE / EDIT (POST)
        [HttpPost]
        public IActionResult Edit(Survey survey) // Survey тепер відомий
        {
            if (ModelState.IsValid)
            {
                repository.SaveSurvey(survey);
                TempData["message"] = $"Опитування '{survey.Title}' було збережено.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(survey);
            }
        }

        // ... (інші методи Details, Delete, DeleteConfirmed) ...
        // Вони також автоматично виправляться, оскільки Survey та ISurveyRepository тепер відомі.
    }
}