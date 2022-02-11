using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class Location
    {
        [Key]
        [Required]
        [Display(Name = "locationId")]
        public string LocationId { get; set; }
        [Required]
        [Display(Name = "locationName")]
        public string LocationName { get; set; }
        
        [Required]
        [Display(Name = "locationCity")]
        public string LocationCity { get; set; }
        [Required]
        [Display(Name = "locationState")]
        public string LocationState { get; set; }
        [Required]
        [Range(0, 1)]
        [Display(Name = "locationCapacity")]
        public int LocationCapacity { get; set; }
        [Required]
        [Display(Name = "locationOpenTime")]
        public DateTime LocationOpenTime { get; set; }
        [Required]
        [Display(Name = "locationCloseTime")]
        public DateTime LocationCloseTime { get; set; }

        [Required]
        [Display(Name = "reservationList")]
        public virtual List<Reservation> Reservations { get; set; }

    }
}
