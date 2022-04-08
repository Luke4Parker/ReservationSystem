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
        public string Id { get; set; }

        [Required]
        [Display(Name = "locationId")]
        public string LocationId { get; set; }

        [Required]
        [Display(Name = "customerId")]
        public string CustomerId { get; set; }

        [Required]
        [Display(Name = "reservationLength")]
        public decimal Length;

        [Required]
        [Display(Name = "partySize")]
        public int PartySize { get; set; }

        [Required]
        [Display(Name = "reservationDateTime")]
        public object ReservationTime { get; set; }

    }
}
