using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Введіть ім'я користувача")]
        [Display(Name = "Ім'я")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Підтвердіть пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}