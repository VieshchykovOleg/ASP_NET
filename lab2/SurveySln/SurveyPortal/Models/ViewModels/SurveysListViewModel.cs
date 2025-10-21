using System.Collections.Generic;

namespace SurveyPortal.Models.ViewModels
{
    public class SurveysListViewModel
    {
        public IEnumerable<Survey> Surveys { get; set; } = Enumerable.Empty<Survey>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
