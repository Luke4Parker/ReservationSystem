using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class CustomerPatch
    {
        [Key]
        [Display(Name = "customerId")]
        public string CustomerId { get; set; }
        
        [Display(Name = "customerFirstName")]
        public string CustomerFirstName { get; set; }
        
        [Display(Name = "customerLastName")]
        public string CustomerLastName { get; set; }
        
        [Display(Name = "customerPhone")]
        public string CustomerPhone { get; set; }
       
        [Display(Name = "customerEmail")]
        public string CustomerEmail { get; set; }

    }
}