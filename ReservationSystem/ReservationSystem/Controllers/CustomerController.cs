using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Daos;
using ReservationSystem.Models;
using System;
using System.Threading.Tasks;

namespace ReservationSystem.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private readonly ReservationSystemDao _dao;
        public CustomerController(ReservationSystemDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerNullable customerQuery)
        {
            try
            {
                var customers = await _dao.GetCustomers(customerQuery);
                return Ok(customers);
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
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerNullable newCustomer)
        {
            try
            {
                await _dao.CreateCustomer(newCustomer);
                return StatusCode(200);
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
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            try
            {
                var customer = await _dao.GetCustomerById(id);
                if (customer == null)
                {
                    return ValidationProblem($"Customer with id {id} was not found.");
                }
                await _dao.DeleteCustomer(id);
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
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerNullable customerUpdates, [FromRoute] int id)
        {
            try
            {
                var customer = await _dao.GetCustomerById(id);
                if (customer == null)
                {
                    return ValidationProblem($"Customer with id {id} was not found.");
                }
                await _dao.UpdateCustomer(customerUpdates, id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
