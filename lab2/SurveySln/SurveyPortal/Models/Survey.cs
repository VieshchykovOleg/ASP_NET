using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyPortal.Models
{
    public class Survey
    {
        public long? SurveyID { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        // Додамо поле для адміністративної інформації
        public string Creator { get; set; } = String.Empty;
        // Приклад числового поля для аналізу
        [Column(TypeName = "decimal(8, 2)")]
        public decimal AverageRating { get; set; }
    }
}