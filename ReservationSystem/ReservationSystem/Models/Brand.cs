using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class Brand
    {
        [Key]
        [Required]
        [Display(Name = "Brand Id")]
        public string BrandId { get; set; }
        [Required]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }
}
