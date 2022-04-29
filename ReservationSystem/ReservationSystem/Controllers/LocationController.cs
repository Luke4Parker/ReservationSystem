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
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLocations([FromQuery] LocationNullable locationQuery)
        {
            try
            {
                var locations = await _dao.GetLocations(locationQuery);
                return Ok(locations);
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
        public async Task<IActionResult> CreateLocation([FromBody] LocationNullable newLocation)
        {
            try
            {
                await _dao.CreateLocation(newLocation);
                return StatusCode(204);
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
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            try
            {
                var location = await _dao.GetLocationById(id);
                if (location == null)
                {
                    return ValidationProblem($"Location with id: {id} was not found.");
                }
                await _dao.DeleteLocation(id);
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
        public async Task<IActionResult> UpdateLocation([FromBody] LocationNullable locationUpdates, [FromRoute] int id)
        {
            try
            {
                var location = await _dao.GetLocationById(id);
                if (location == null)
                {
                    return ValidationProblem($"Location with id: {id} was not found.");
                }
                await _dao.UpdateLocation(locationUpdates, id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
