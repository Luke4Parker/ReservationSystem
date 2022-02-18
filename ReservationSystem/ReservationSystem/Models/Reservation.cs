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
        [Display(Name = "reservationLength")]
        public TimeSpan ReservationLength = new TimeSpan(0, 1, 0, 0);

        [Required]
        [Display(Name = "partySize")]
        public int PartySize { get; set; }

        [Required]
        [Display(Name = "reservationDateTime")]
        public DateTime ReservationDateTime { get; set; }

    }
}
