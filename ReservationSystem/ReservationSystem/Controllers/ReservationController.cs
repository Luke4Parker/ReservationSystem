﻿using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetReservations()
        {
            try
            {
                var reservations = await _dao.GetReservations();
                return Ok(reservations);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservationById([FromRoute] int id)
        {
            try
            {
                var reservation = await _dao.GetReservationById(id);
                if (reservation == null)
                {
                    return StatusCode(404);
                }
                return Ok(reservation);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("customer/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservationByCustomerId([FromRoute] int customerId)
        {
            try
            {
                var reservations = await _dao.GetReservationByCustomerId(customerId);
                if (reservations == null)
                {
                    return StatusCode(404);
                }
                return Ok(reservations);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("location/{locationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservationByLocationId([FromRoute] int locationId)
        {
            try
            {
                var reservations = await _dao.GetReservationByLocationId(locationId);
                if (reservations == null)
                {
                    return StatusCode(404);
                }
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
        public async Task<IActionResult> CreateReservation([FromBody] Reservation newReservation)
        {
            int capacityCounter = newReservation.PartySize;
            try
            {
                var reservations = await _dao.GetReservationByLocationId(int.Parse(newReservation.LocationId));
                var location = await _dao.GetLocationById(int.Parse(newReservation.LocationId));
                if (DateTime.Parse(newReservation.ReservationTime.ToString()).Hour >= DateTime.Parse(location.OpenTime.ToString()).Hour &&
                    DateTime.Parse(newReservation.ReservationTime.ToString()).Hour < DateTime.Parse(location.CloseTime.ToString()).Hour)
                {
                    if (capacityCounter > location.Capacity)
                    {
                        return ValidationProblem($"This location cannot host a party of this size. Maximum party size allowed is {location.Capacity}");
                    }
                    List<Reservation> overlappingReservationsList = new List<Reservation>();
                    foreach (Reservation r in reservations)
                    {
                        capacityCounter = newReservation.PartySize;
                        int overlappingCapacityCount = newReservation.PartySize;
                        if (r.CheckOverlap(newReservation, r))
                        {
                            foreach (Reservation overlap in overlappingReservationsList)
                            {

                                if (r.CheckOverlap(r, overlap))
                                {
                                    overlappingCapacityCount += overlap.PartySize + r.PartySize;
                                    if (overlappingCapacityCount > location.Capacity)
                                    {
                                        return ValidationProblem($"Not enough tables available at this time [Overlap].");
                                    }
                                }
                            }
                            overlappingReservationsList.Add(r);
                            capacityCounter += r.PartySize;
                            if (capacityCounter > location.Capacity)
                            {
                                return ValidationProblem($"Not enough tables available at this time.");
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
        public async Task<IActionResult> UpdateReservation([FromBody] Reservation reservationUpdates, [FromRoute] int id)
        {
            try
            {
                var reservation = await _dao.GetReservationById(id);
                if (reservation == null)
                {
                    return StatusCode(404);
                }
                await _dao.UpdateReservation(reservationUpdates, id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
