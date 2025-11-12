using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models;

namespace SurveyPortal.Controllers
{
	// Тут можна додати [Authorize] для захисту адмінки
	public class AdminController : Controller
	{
		private ISurveyRepository repository;

		public AdminController(ISurveyRepository repo)
		{
			repository = repo;
		}

		// READ (List)
		public IActionResult Index() => View(repository.Surveys);

		// READ (Details)
		public IActionResult Details(long id)
		{
			Survey? survey = repository.Surveys
				.FirstOrDefault(s => s.SurveyID == id);

			if (survey == null)
			{
				return NotFound();
			}
			return View(survey);
		}

		// CREATE (GET) - Показати форму
		public IActionResult Create() => View("Edit", new Survey());

		// EDIT (GET) - Показати форму
		public IActionResult Edit(long id)
		{
			Survey? survey = repository.Surveys
				.FirstOrDefault(s => s.SurveyID == id);

			if (survey == null)
			{
				return NotFound();
			}
			return View(survey);
		}

		// CREATE / EDIT (POST) - Обробити форму
		[HttpPost]
		public IActionResult Edit(Survey survey)
		{
			// Ми додамо валідацію у Завданні 3, 
			// але перевірка ModelState.IsValid вже має бути тут
			if (ModelState.IsValid)
			{
				repository.SaveSurvey(survey);
				TempData["message"] = $"Опитування '{survey.Title}' було збережено.";
				return RedirectToAction("Index");
			}
			else
			{
				// Щось не так з даними, повернути форму
				return View(survey);
			}
		}

		// DELETE (GET) - Показати сторінку підтвердження
		public IActionResult Delete(long id)
		{
			Survey? survey = repository.Surveys
				.FirstOrDefault(s => s.SurveyID == id);

			if (survey == null)
			{
				return NotFound();
			}
			return View(survey);
		}

		// DELETE (POST) - Виконати видалення
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(long surveyID)
		{
			Survey? deletedSurvey = repository.DeleteSurvey(surveyID);

			if (deletedSurvey != null)
			{
				TempData["message"] = $"Опитування '{deletedSurvey.Title}' було видалено.";
			}
			return RedirectToAction("Index");
		}
	}
}