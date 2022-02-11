using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class Brand
    {
        [Key]
        [Required]
        [Display(Name = "brandId")]
        public string BrandId { get; set; }
        [Required]
        [Display(Name = "brandName")]
        public string BrandName { get; set; }

        [Required]
        [Display(Name = "locations")]
        public virtual List<Location> Locations { get; set; }
    }
}
