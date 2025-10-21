using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models;
using SurveyPortal.Models.ViewModels; // ������� using

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
            var itemsPerPage = 4; // ʳ������ �������� �� �������

            return View(new SurveysListViewModel
            {
                Surveys = repository.Surveys
                    .OrderBy(s => s.SurveyID) // ���������� ��� ���������� ���������
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
