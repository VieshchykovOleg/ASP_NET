using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models;
using SurveyPortal.Models.ViewModels;
using SurveyPortal.Data.Models;

namespace SurveyPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISurveyRepository repository;

        public HomeController(ISurveyRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index(string? category, int surveyPage = 1)
        {
            int itemsPerPage = 4;

            var surveys = repository.Surveys
                .Where(s => category == null || s.Category == category)
                .OrderBy(s => s.SurveyID);

            var model = new SurveysListViewModel
            {
                Surveys = surveys
                    .Skip((surveyPage - 1) * itemsPerPage)
                    .Take(itemsPerPage),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = surveyPage,
                    ItemsPerPage = itemsPerPage,
                    TotalItems = surveys.Count()
                },

                CurrentCategory = category
            };

            return View(model);
        }
    }
}
