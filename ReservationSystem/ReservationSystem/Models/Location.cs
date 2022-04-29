using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class Location
    {
        [Key]
        [Required]
        [Display(Name = "Location Id")]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Location Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Location City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Location State")]
        public string State { get; set; }
        [Required]
        [Range(0, 1000)]
        [Display(Name = "Location Capacity")]
        public int Capacity { get; set; }
        [Required]
        [Display(Name = "Location Open Time")]
        public object OpenTime { get; set; } 
        [Required]
        [Display(Name = "Location Close Time")]
        public object CloseTime { get; set; }
        [Required]
        [Display(Name = "Brand Id")]
        public string BrandId { get; set; }

    }
}
