using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models;
using SurveyPortal.Models.ViewModels; // Додайте using

namespace SurveyPortal.Controllers
{
    public class HomeController : Controller
    {
        private ISurveyRepository repository;
        public HomeController(ISurveyRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index(int surveyPage = 1)
        {
            var itemsPerPage = 4; // Кількість елементів на сторінку

            return View(new SurveysListViewModel
            {
                Surveys = repository.Surveys
                    .OrderBy(s => s.SurveyID) // Сортування для стабільного пейджингу
                    .Skip((surveyPage - 1) * itemsPerPage)
                    .Take(itemsPerPage),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = surveyPage,
                    ItemsPerPage = itemsPerPage,
                    TotalItems = repository.Surveys.Count()
                }
            });
        }
    }
}
