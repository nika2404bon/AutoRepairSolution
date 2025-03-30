using System.ComponentModel.DataAnnotations;

namespace AutoRepairService.Data.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название услуги обязательно для заполнения")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Название должно содержать от 2 до 100 символов")]
        [Display(Name = "Название услуги")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        [Display(Name = "Описание")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Укажите цену услуги")]
        [Range(0.01, 1000000, ErrorMessage = "Цена должна быть в диапазоне от 0,01 до 1 000 000")]
        [Display(Name = "Цена (₽)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Укажите длительность услуги")]
        [Range(1, 1440, ErrorMessage = "Длительность должна быть от 1 до 1440 минут (24 часа)")]
        [Display(Name = "Длительность (мин)")]
        public int DurationInMinutes { get; set; }
    }
}