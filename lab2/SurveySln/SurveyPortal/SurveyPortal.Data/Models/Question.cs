using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.Data.Models
{
    public class Question
    {
        public long QuestionID { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть текст питання")]
        [Display(Name = "Текст питання")]
        public string Text { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, оберіть тип питання")]
        [Display(Name = "Тип питання")]
        public string QuestionType { get; set; } = "Text";

        [Required]
        public long SurveyID { get; set; }

        public Survey? Survey { get; set; }
    }
}