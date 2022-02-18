using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Daos;
using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class LocationController : ControllerBase
    {
        private readonly ReservationSystemDao _dao;
        public LocationController(ReservationSystemDao dao)
        {
            _dao = dao;
            if (_dao.Locations.Any()) return;

            ReservationSeed.InitData(dao);
        }

        //Get requests - This section contains Get Requests for location and reservations
        //**************************
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IQueryable<Location>> GetLocations([FromQuery] string locationName)
        {
            var result = _dao.Locations as IQueryable<Location>;

            if (!string.IsNullOrEmpty(locationName))
            {
                result = result.Where(l => l.LocationName.Contains(locationName, StringComparison.InvariantCultureIgnoreCase));
            }

            return Ok(result.OrderBy(l => l.LocationId));
        }
        //[HttpGet]
        //[Route("{locationId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IQueryable<Location>> GetLocationsById([FromQuery] string locationId)
        //{
        //    var result = _dao.Locations as IQueryable<Location>;

        //    if (!string.IsNullOrEmpty(locationId))
        //    {
        //        result = result.Where(l => l.LocationId.Contains(locationId, StringComparison.InvariantCultureIgnoreCase));
        //    }

        //    return Ok(result.OrderBy(l => l.LocationId));
        //}


        [HttpGet]
        [Route("{locationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<IQueryable<Reservation>> GetReservationById([FromRoute] string locationId, [FromQuery] string reservationId)
        public ActionResult GetReservationById([FromRoute] string locationId)
        {
            var result = _dao.Locations as IQueryable<Location>;
            var reservations = new List<Reservation>();

            if (!string.IsNullOrEmpty(locationId))
            {
                var location = result.First(l => l.LocationId.Equals(locationId));
                reservations = location.Reservations;
            }

            return Ok(reservations.OrderBy(r => r.ReservationId));
        }
        //END GET REQUESTS
        //*********************


        //START POST REQUESTS
        //*********************
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Location> PostLocation([FromBody] Location location)
        {
            try
            {
                _dao.Locations.Add(location);
                _dao.SaveChanges();

                return new CreatedResult($"/locations/{location.LocationId.ToLower()}", location);
            }
            catch (Exception e)
            {
                return ValidationProblem(e.Message);
            }
        }

        [HttpPost]
        [Route("{locationId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PostReservation([FromRoute] string locationId, [FromBody] Reservation reservation)
        {
            try
            {
                var location = _dao.Locations.First(l => l.LocationId.Equals(locationId));
                location.Reservations.Add(reservation);

                _dao.SaveChanges();

                return new CreatedResult($"/locations/{location.LocationId.ToLower()}", location);
            }
            catch (Exception e)
            {
                return ValidationProblem(e.Message);
            }

        }

        //END POST REQUESTS
        //*********************


        //START DELETE REQUESTS
        //*********************
        [HttpDelete]
        [Route("{locationId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Location> DeleteLocation([FromRoute] string locationId)
        {
            try
            {
                var locationList = _dao.Locations as IQueryable<Location>;
                var location = locationList.First(l => l.LocationId.Equals(locationId));
                _dao.Locations.Remove(location);
                _dao.SaveChanges();

                return new CreatedResult($"/locations/{location.LocationId.ToLower()}", location);
            }
            catch (Exception e)
            {
                return ValidationProblem(e.Message);
            }
        }

        [HttpDelete]
        [Route("{locationId}/reservations")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Location> DeleteReservation([FromRoute] string locationId, [FromQuery] string reservationId)
        {
            try
            {
                var locationList = _dao.Locations as IQueryable<Location>;
                var location = locationList.First(l => l.LocationId.Equals(locationId));
                location.Reservations.Remove(location.Reservations.First(r => r.ReservationId.Equals(reservationId)));

                _dao.SaveChanges();

                return new CreatedResult($"/locations/{location.LocationId.ToLower()}", location);
            }
            catch (Exception e)
            {
                return ValidationProblem(e.Message);
            }
        }
        //END DELETE REQUESTS
        //**********************

        //START PATCH REQUESTS
        //**********************
        [HttpPatch]
        [Route("{locationId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Location> PatchLocation([FromRoute] string locationId, [FromBody] LocationPatch newLocation)
        {
            try
            {
                var locationList = _dao.Locations as IQueryable<Location>;
                var location = locationList.First(p => p.LocationId.Equals(locationId));

                location.LocationName = newLocation.LocationName ?? location.LocationName;
                location.LocationCity = newLocation.LocationCity ?? location.LocationCity;
                location.LocationState = newLocation.LocationState ?? location.LocationState;
                string temp = location.LocationCapacity.ToString();
                temp = newLocation.LocationCapacity.ToString() ?? location.LocationCapacity.ToString();
                location.LocationCapacity = int.Parse(temp);
                location.LocationOpenTime = newLocation.LocationOpenTime ?? location.LocationOpenTime;
                location.LocationCloseTime = newLocation.LocationCloseTime ?? location.LocationOpenTime;
                //location.Reservations = newLocation.Reservations ?? location.Reservations; 

                _dao.Locations.Update(location);
                _dao.SaveChanges();

                return new CreatedResult($"/locations/{location.LocationId.ToLower()}", location);
            }
            catch (Exception e)
            {
                // Typically an error log is produced here
                return ValidationProblem(e.Message);
            }
        }

        [HttpPatch]
        [Route("{locationId}/{reservationId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Location> PatchReservation([FromRoute] string locationId, [FromRoute] string reservationId, [FromBody] ReservationPatch newReservation)
        {
            try
            {
                var locationList = _dao.Locations as IQueryable<Location>;
                var location = locationList.First(p => p.LocationId.Equals(locationId));
                var reservation = location.Reservations.First(r => r.ReservationId.Equals(reservationId));
                
                string temp = newReservation.ReservationLength.ToString() ?? reservation.ReservationLength.ToString();
                reservation.ReservationLength = TimeSpan.Parse(temp);

                temp = newReservation.PartySize.ToString() ?? reservation.PartySize.ToString();
                reservation.PartySize = int.Parse(temp);

                temp = newReservation.ReservationDateTime.ToString() ?? reservation.ReservationDateTime.ToString();
                reservation.ReservationDateTime = DateTime.Parse(temp);

                location.Reservations.Remove(location.Reservations.First(r => r.ReservationId.Equals(reservationId)));
                location.Reservations.Add(reservation);
                _dao.Locations.Update(location);
                _dao.SaveChanges();

                return new CreatedResult($"/locations/{location.LocationId.ToLower()}", location);
            }
            catch (Exception e)
            {
                // Typically an error log is produced here
                return ValidationProblem(e.Message);
            }
        }
        //END PATCH REQUESTS
        //********************
    }
}
