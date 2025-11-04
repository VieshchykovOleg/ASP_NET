
using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models;
using SurveyPortal.Models.ViewModels; // Переконайся, що цей using є

namespace SurveyPortal.Controllers
{
    public class HomeController : Controller
    {
        private ISurveyRepository repository;
        public int itemsPerPage = 4;

        public HomeController(ISurveyRepository repo)
        {
            repository = repo;
        }

        // ОНОВИ МЕТОД Index
        public IActionResult Index(string? category, int surveyPage = 1)
        {
            // Фільтруємо запит
            var filteredSurveys = repository.Surveys
                .Where(s => category == null || s.Category == category);

            return View(new SurveysListViewModel
            {
                Surveys = filteredSurveys
                    .OrderBy(s => s.SurveyID) // Сортування
                    .Skip((surveyPage - 1) * itemsPerPage)
                    .Take(itemsPerPage),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = surveyPage,
                    ItemsPerPage = itemsPerPage,
                    // Загальна кількість елементів ТАКОЖ має бути відфільтрована
                    TotalItems = filteredSurveys.Count()
                },

                // Передаємо поточну категорію у ViewModel
                CurrentCategory = category
            });
        }
    }
}