using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class CustomerNullable
    {
        [Display(Name = "customerFirstName")]
        public string? FirstName { get; set; }

        [Display(Name = "customerLastName")]
        public string? LastName { get; set; }

        [Display(Name = "customerPhone")]
        public string? Phone { get; set; }

        [Display(Name = "customerEmail")]
        public string? Email { get; set; }

    }
}
