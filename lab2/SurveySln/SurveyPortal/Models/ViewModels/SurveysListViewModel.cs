using System.Collections.Generic;
using SurveyPortal.Data.Models;
using SurveyPortal.Shared;

namespace SurveyPortal.Models.ViewModels
{
    public class SurveysListViewModel
    {
        public IEnumerable<Survey> Surveys { get; set; } = Enumerable.Empty<Survey>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
        public string? CurrentCategory { get; set; }
    }
}