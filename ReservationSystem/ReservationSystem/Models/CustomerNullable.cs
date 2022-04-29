using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class CustomerNullable
    {
        [Display(Name = "Customer First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Customer Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Customer Phone")]
        public string? Phone { get; set; }

        [Display(Name = "Customer Email")]
        public string? Email { get; set; }

    }
}
