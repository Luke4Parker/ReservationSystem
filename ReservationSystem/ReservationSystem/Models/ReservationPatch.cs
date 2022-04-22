using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class ReservationPatch
    {
        [Key]
        [Display(Name = "reservationId")]
        public string ReservationId { get; set; }

        [Display(Name = "locationId")]
        public string LocationId { get; set; }

        [Display(Name = "customerId")]
        public string CustomerId { get; set; }

        [Display(Name = "length")]
        public TimeSpan ReservationLength = new TimeSpan(0, 1, 0, 0);

        [Display(Name = "partySize")]
        public int PartySize { get; set; }

        [Display(Name = "ReservationDateTime")]
        public DateTime ReservationDateTime { get; set; }
    }
}
