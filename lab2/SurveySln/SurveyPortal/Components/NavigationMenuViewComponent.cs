using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models;

namespace SurveyPortal.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly ISurveyRepository _repository;

        public NavigationMenuViewComponent(ISurveyRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            var categories = _repository.Surveys
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(categories);
        }
    }
}
