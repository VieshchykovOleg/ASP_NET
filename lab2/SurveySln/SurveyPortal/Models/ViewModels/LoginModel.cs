using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "¬вед≥ть ≥м'€ користувача")]
        [Display(Name = "≤м'€")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "¬вед≥ть пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "ѕароль")]
        public string Password { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = "/";
    }
}