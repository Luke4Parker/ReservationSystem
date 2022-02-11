using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


    namespace ReservationSystem.Models
    {
        public class LocationPatch
        {
            
            [Display(Name = "locationId")]
            public string LocationId { get; set; }
            
            [Display(Name = "locationName")]
            public string LocationName { get; set; }

            
            [Display(Name = "locationCity")]
            public string LocationCity { get; set; }
            
            [Display(Name = "locationState")]
            public string LocationState { get; set; }
            
            [Range(0, 1000)]
            [Display(Name = "locationCapacity")]
            public int LocationCapacity { get; set; }

            
            [Display(Name = "locationOpenTime")]
            public string LocationOpenTime { get; set; }
            
            [Display(Name = "locationCloseTime")]
            public string LocationCloseTime { get; set; }

            [Display(Name = "reservationList")]
            public virtual List<Reservation> Reservations { get; set; }

        }
    }
