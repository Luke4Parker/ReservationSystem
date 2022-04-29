using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class ReservationNullable
    {

        [Display(Name = "Location Id")]
        public string? LocationId { get; set; }

        [Display(Name = "Customer Id")]
        public string? CustomerId { get; set; }

        [Display(Name = "Reservation Length")]
        public float? Length { get; set; }

        [Display(Name = "Party Size")]
        public int? PartySize { get; set; }

        [Display(Name = "Reservation Date Time")]
        public object? ReservationTime { get; set; }

    }
}
