using Microsoft.AspNetCore.Mvc;
using SurveyPortal.Models;
using SurveyPortal.Infrastructure;

namespace SurveyPortal.Controllers
{
	public class SurveySessionController : Controller
	{
		private readonly ISurveyRepository repository;

		public SurveySessionController(ISurveyRepository repo)
		{
			repository = repo;
		}

		public IActionResult Index(string returnUrl)
		{
			var session = HttpContext.Session.GetJson<SurveySession>("SurveySession") ?? new SurveySession();

			ViewBag.ReturnUrl = returnUrl;

			return View(session);
		}

		[HttpPost]
		public IActionResult AddToSession(int surveyId, string returnUrl)
		{
			var survey = repository.Surveys.FirstOrDefault(s => s.SurveyID == surveyId);
			if (survey != null)
			{
				var session = HttpContext.Session.GetJson<SurveySession>("SurveySession") ?? new SurveySession();
				session.AddSurvey(survey);
				HttpContext.Session.SetJson("SurveySession", session);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		[HttpPost]
		public IActionResult RemoveFromSession(int surveyId, string returnUrl)
		{
			var session = HttpContext.Session.GetJson<SurveySession>("SurveySession") ?? new SurveySession();
			session.RemoveSurvey(surveyId);
			HttpContext.Session.SetJson("SurveySession", session);

			return RedirectToAction("Index", new { returnUrl });
		}
	}
}
