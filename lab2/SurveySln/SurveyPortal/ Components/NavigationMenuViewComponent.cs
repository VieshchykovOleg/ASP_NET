using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models; // Додай using для ISurveyRepository

namespace SurveyPortal.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ISurveyRepository repository;

        // Впровадження репозиторію
        public NavigationMenuViewComponent(ISurveyRepository repo)
        {
            repository = repo;
        }

        // Оновлений метод Invoke
        public IViewComponentResult Invoke()
        {
            // Збережемо обрану категорію у ViewBag для виділення
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            // Отримуємо список унікальних категорій
            var categories = repository.Surveys
                .Select(s => s.Category)
                .Distinct()
                .OrderBy(c => c);

            return View(categories);
        }
    }
}