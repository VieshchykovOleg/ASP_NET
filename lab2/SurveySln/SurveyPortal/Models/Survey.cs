using System.ComponentModel.DataAnnotations.Schema;
namespace SurveyPortal.Models

{
    public class Survey
    {
        public long SurveyID { get; set; }     
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Creator { get; set; } = "";
        public decimal AverageRating { get; set; }  
        public string Category { get; set; } = "";

        // Зв'язок Один-до-Багатьох
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
