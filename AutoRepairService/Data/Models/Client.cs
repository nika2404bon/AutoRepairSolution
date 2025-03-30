using System.ComponentModel.DataAnnotations;

namespace AutoRepairService.Data.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле 'Фамилия' обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле 'Телефон' обязательно для заполнения")]
        [Phone(ErrorMessage = "Некорректный формат телефона")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
    }
}