﻿using System;
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
        public string Id { get; set; }
        [Required]
        [Display(Name="customerFirstName")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "customerLastName")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "customerPhone")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "customerEmail")]
        public string Email { get; set; }

    }
}
