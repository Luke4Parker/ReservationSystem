using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class Customer
    {
        [Key]
        [Required]
        [Display(Name="customerId")]
        public string CustomerId { get; set; }
        [Required]
        [Display(Name="customerFirstName")]
        public string CustomerFirstName { get; set; }
        [Required]
        [Display(Name = "customerLastName")]
        public string CustomerLastName { get; set; }
        [Required]
        [Display(Name = "customerPhone")]
        public string CustomerPhone { get; set; }
        [Required]
        [Display(Name = "customerEmail")]
        public string CustomerEmail { get; set; }

    }
}
