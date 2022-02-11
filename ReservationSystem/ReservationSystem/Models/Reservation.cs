using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class Reservation
    {
        [Required]
        [Display(Name = "reservationId")]
        public string ReservationId { get; set; }

        [Required]
        [Display(Name = "locationId")]
        public string LocationId { get; set; }

        [Required]
        [Display(Name = "customerId")]
        public string CustomerId { get; set; }

        [Required]
        [Display(Name = "length")]
        public TimeSpan ReservationLength { get; set; }

        [Required]
        [Display(Name = "partySize")]
        public int PartySize { get; set; }

        [Required]
        [Display(Name = "ReservationDateTime")]
        public DateTime ReservationDateTime { get; set; }

    }
}
