using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationSystem.Daos;
using ReservationSystem.Models;

namespace ReservationSystem.Models
{
    public class ReservationSeed
    {
        public static void InitData(ReservationSystemDao dao)
        {
            Customer customer1 = new Customer();
            customer1.CustomerId = "1";
            customer1.CustomerFirstName = "Randy";
            customer1.CustomerLastName = "Jenks";
            customer1.CustomerPhone = "1534567890";
            customer1.CustomerEmail = "jenkinrando42@gmail.com";

            dao.Customers.Add(customer1);

            Reservation reservation1 = new Reservation();
            reservation1.ReservationId = "1";
            reservation1.CustomerId = "1";
            reservation1.LocationId = "1";
            //reservation1.ReservationLength = new TimeSpan(0, 1, 0, 0);
            reservation1.PartySize = 1;
            reservation1.ReservationDateTime = new DateTime(2022, 2, 14, 17, 30, 00);

            Reservation reservation2 = new Reservation();
            reservation2.ReservationId = "2";
            reservation2.CustomerId = "1";
            reservation2.LocationId = "1";
            //reservation2.ReservationLength = new TimeSpan(0, 1, 0, 0);
            reservation2.PartySize = 1;
            reservation2.ReservationDateTime = new DateTime(2023, 2, 14, 17, 30, 00);

            List<Reservation> reservations = new List<Reservation>();
            reservations.Add(reservation1);
            reservations.Add(reservation2);


            Location location1 = new Location();
            location1.LocationId = "1";
            location1.LocationName = "Huberts Gas Station Sushi";
            location1.LocationCity = "St. Johnsbury";
            location1.LocationState = "Vermont";
            location1.LocationOpenTime = "10:00:00";
            location1.LocationCloseTime = "19:00:00";
            location1.LocationCapacity = 1;
            location1.Reservations = reservations;

            dao.Locations.Add(location1);

            dao.SaveChanges();


        }
    }
}
