using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class LocationNullable
    {


        [Display(Name = "Location Name")]
        public string? Name { get; set; }

        [Display(Name = "Location City")]
        public string? City { get; set; }

        [Display(Name = "Location State")]
        public string? State { get; set; }

        [Range(0, 1000)]
        [Display(Name = "Location Capacity")]
        public int? Capacity { get; set; }

        [Display(Name = "Location Open Time")]
        public string? OpenTime { get; set; }

        [Display(Name = "Location Close Time")]
        public string? CloseTime { get; set; }

        [Display(Name = "Brand Id")]
        public string? BrandId { get; set; }

    }
}
