using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyPortal.Data.Models
{
    public class Survey
    {
        public long SurveyID { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть назву опитування")]
        [Display(Name = "Назва")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть опис")]
        [Display(Name = "Опис")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть автора")]
        [Display(Name = "Автор")]
        public string Creator { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть категорію")]
        [Display(Name = "Категорія")]
        public string Category { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 5.00, ErrorMessage = "Рейтинг має бути між 0.01 та 5.00")]
        [Column(TypeName = "decimal(8, 2)")]
        [Display(Name = "Середній рейтинг")]
        public decimal AverageRating { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}