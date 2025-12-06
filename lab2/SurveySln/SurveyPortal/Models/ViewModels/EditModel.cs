using System.ComponentModel.DataAnnotations;

namespace SurveyPortal.Models.ViewModels
{
    public class EditModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть Email")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль (необов'язково)")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження пароля")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string? ConfirmPassword { get; set; }
    }
}