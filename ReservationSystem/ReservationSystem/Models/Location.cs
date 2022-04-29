using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class Location
    {
        [Key]
        [Required]
        [Display(Name = "locationId")]
        public string Id { get; set; }
        [Required]
        [Display(Name = "locationName")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "locationCity")]
        public string City { get; set; }
        [Required]
        [Display(Name = "locationState")]
        public string State { get; set; }
        [Required]
        [Range(0, 1000)]
        [Display(Name = "locationCapacity")]
        public int Capacity { get; set; }
        [Required]
        [Display(Name = "locationOpenTime")]
        public object OpenTime { get; set; } 
        [Required]
        [Display(Name = "locationCloseTime")]
        public object CloseTime { get; set; }
        [Required]
        [Display(Name = "brandId")]
        public int BrandId { get; set; }

    }
}
