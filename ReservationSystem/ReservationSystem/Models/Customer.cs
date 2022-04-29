using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class Customer
    {
        [Key]
        [Required]
        [Display(Name="Customer Id")]
        public string Id { get; set; }

        [Required]
        [Display(Name="Customer First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Customer Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Customer Phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Customer Email")]
        public string Email { get; set; }

    }
}
