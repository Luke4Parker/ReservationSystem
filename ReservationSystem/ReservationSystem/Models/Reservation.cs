using System;
using System.ComponentModel.DataAnnotations;

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
        public float Length { get; set; }

        [Required]
        [Display(Name = "partySize")]
        public int PartySize { get; set; }

        [Required]
        [Display(Name = "reservationDateTime")]
        public object ReservationTime { get; set; }


        public static bool CheckOverlap(Reservation newReservation, Reservation existingReservation)
        {
            var nrTime = DateTime.Parse(newReservation.ReservationTime.ToString());
            var erTime = DateTime.Parse(existingReservation.ReservationTime.ToString());

            //check if new reservation starts during existing reservation            
            if (nrTime < erTime.Add(TimeSpan.FromHours(existingReservation.Length)) &&
                nrTime >= erTime)
            {
                return true;
            }

            //check if new reservation will be long enough to overlap with existing reservation
            else if (nrTime >= erTime.Subtract(TimeSpan.FromHours(newReservation.Length)) &&
                nrTime <= erTime)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

    
}
