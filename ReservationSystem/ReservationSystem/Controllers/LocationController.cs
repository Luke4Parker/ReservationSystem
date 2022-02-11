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
    }
}
