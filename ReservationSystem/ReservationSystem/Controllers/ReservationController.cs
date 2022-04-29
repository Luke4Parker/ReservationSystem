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
    public class ReservationController : ControllerBase
    {
        private readonly ReservationSystemDao _dao;

        public ReservationController(ReservationSystemDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservations([FromQuery] ReservationNullable reservationQuery)
        {
            try
            {
                var reservations = await _dao.GetReservations(reservationQuery);
                return Ok(reservations);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationNullable newReservation)
        {
            try
            {
                int capacityCounter = (int)newReservation.PartySize;
                var reservations = await _dao.GetReservationByLocationId(int.Parse(newReservation.LocationId));
                var location = await _dao.GetLocationById(int.Parse(newReservation.LocationId));

                //Validates that reservations aren't added outside operating hours
                if (DateTime.Parse(newReservation.ReservationTime.ToString()).Hour >= DateTime.Parse(location.OpenTime.ToString()).Hour &&
                    DateTime.Parse(newReservation.ReservationTime.ToString()).Hour < DateTime.Parse(location.CloseTime.ToString()).Hour)
                {
                    if (capacityCounter > location.Capacity)
                    {
                        return ValidationProblem($"This location cannot host a party of this size. Maximum party size allowed is {location.Capacity}");
                    }

                    if (reservations.Count() > 0)
                    {
                        List<Reservation> overlappingReservationsList = new List<Reservation>();
                        foreach (Reservation r in reservations)
                        {
                            try
                            {
                                int overlappingCapacityCount = (int)newReservation.PartySize;

                                if (ReservationNullable.CheckOverlap(newReservation, r))
                                {
                                    if (overlappingReservationsList.Count() < 1)
                                    {
                                        overlappingReservationsList.Add(r);
                                        overlappingCapacityCount += r.PartySize;
                                        if (overlappingCapacityCount > location.Capacity)
                                        {
                                            return ValidationProblem($"Not enough tables available at this time.");
                                        }
                                    }
                                    else
                                    {
                                        overlappingCapacityCount += r.PartySize;
                                        foreach (Reservation overlap in overlappingReservationsList)
                                        {

                                            if (Reservation.CheckOverlap(r, overlap))
                                            {
                                                overlappingCapacityCount += overlap.PartySize;
                                                if (overlappingCapacityCount > location.Capacity)
                                                {
                                                    return ValidationProblem($"Not enough tables available at this time.");
                                                }
                                            }
                                        }

                                        overlappingReservationsList.Add(r);
                                    }
                                }
                            }
                            catch (ArgumentNullException)
                            {
                                return ValidationProblem("Party size cannot be null");
                            }
                        } 
                    }
                    
                    await _dao.CreateReservation(newReservation);
                    return StatusCode(200);

                }
                else
                {
                    return ValidationProblem($"Adding Reservation Outside Operating Hours. Operating Hours are {location.OpenTime} - {location.CloseTime}");
                }
            }
            catch (NullReferenceException)
            {
                return ValidationProblem("Reservation attributes cannot be null");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReservation([FromRoute] int id)
        {
            try
            {
                var reservation = await _dao.GetReservationById(id);
                if (reservation == null)
                {
                    return StatusCode(404);
                }
                await _dao.DeleteReservation(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateReservation([FromBody] ReservationNullable reservationUpdates, [FromRoute] int id)
        {
            var reservation = await _dao.GetReservationById(id);
            if (reservation == null)
            {
                return ValidationProblem($"Reservation with id:{id} was not found.");
            }

            try
            {
                int capacityCounter = (int)reservationUpdates.PartySize;
                var reservations = await _dao.GetReservationByLocationId(int.Parse(reservationUpdates.LocationId));
                var location = await _dao.GetLocationById(int.Parse(reservationUpdates.LocationId));

                //Validates that reservations aren't added outside operating hours
                if (DateTime.Parse(reservationUpdates.ReservationTime.ToString()).Hour >= DateTime.Parse(location.OpenTime.ToString()).Hour &&
                    DateTime.Parse(reservationUpdates.ReservationTime.ToString()).Hour < DateTime.Parse(location.CloseTime.ToString()).Hour)
                {
                    if (capacityCounter > location.Capacity)
                    {
                        return ValidationProblem($"This location cannot host a party of this size. Maximum party size allowed is {location.Capacity}");
                    }

                    if (reservations.Count() > 0)
                    {
                        List<Reservation> overlappingReservationsList = new List<Reservation>();
                        foreach (Reservation r in reservations)
                        {
                            try
                            {
                                int overlappingCapacityCount = (int)reservationUpdates.PartySize;

                                if (ReservationNullable.CheckOverlap(reservationUpdates, r))
                                {
                                    if (overlappingReservationsList.Count() < 1)
                                    {
                                        overlappingReservationsList.Add(r);
                                        overlappingCapacityCount += r.PartySize;
                                        if (overlappingCapacityCount > location.Capacity)
                                        {
                                            return ValidationProblem($"Not enough tables available at this time.");
                                        }
                                    }
                                    else
                                    {
                                        overlappingCapacityCount += r.PartySize;
                                        foreach (Reservation overlap in overlappingReservationsList)
                                        {

                                            if (Reservation.CheckOverlap(r, overlap))
                                            {
                                                overlappingCapacityCount += overlap.PartySize;
                                                if (overlappingCapacityCount > location.Capacity)
                                                {
                                                    return ValidationProblem($"Not enough tables available at this time.");
                                                }
                                            }
                                        }

                                        overlappingReservationsList.Add(r);
                                    }
                                }
                            }
                            catch (ArgumentNullException)
                            {
                                return ValidationProblem("Party size cannot be null");
                            }                            
                        }
                    }

                    await _dao.UpdateReservation(reservationUpdates, id);
                    return StatusCode(200);

                }
                else
                {
                    return ValidationProblem($"Adding Reservation Outside Operating Hours. Operating Hours are {location.OpenTime} - {location.CloseTime}");
                }
            }
            catch (NullReferenceException)
            {
                return ValidationProblem("Reservation attributes cannot be null");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
