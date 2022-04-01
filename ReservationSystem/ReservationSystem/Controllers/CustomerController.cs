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
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var customers = await _dao.GetCustomers();
                return Ok(customers);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //public ActionResult<IQueryable<Customer>> GetCustomers([FromQuery] string customerId)
        //{
        //    var result = _dao.Customers as IQueryable<Customer>;

        //    if (!string.IsNullOrEmpty(customerId))
        //    {
        //        result = result.Where(l => l.CustomerId.Contains(customerId, StringComparison.InvariantCultureIgnoreCase));
        //    }

        //    return Ok(result.OrderBy(l => l.CustomerId));
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        //public ActionResult<Customer> PostCustomer([FromBody] Customer customer)
        //{
        //    try
        //    {
        //        _dao.Customers.Add(customer);
        //        _dao.SaveChanges();

        //        return new CreatedResult($"/customers/{customer.CustomerId.ToLower()}", customer);
        //    }
        //    catch (Exception e)
        //    {
        //        return ValidationProblem(e.Message);
        //    }
        //}

        //[HttpDelete]
        //[Route("{customerId}")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<Customer> DeleteCustomer([FromRoute] string customerId)
        //{
        //    try
        //    {
        //        var customerList = _dao.Customers as IQueryable<Customer>;
        //        var customer = customerList.First(l => l.CustomerId.Equals(customerId));
        //        _dao.Customers.Remove(customer);
        //        _dao.SaveChanges();

        //        return new CreatedResult($"/customers/{customer.CustomerId.ToLower()}", customer);
        //    }
        //    catch (Exception e)
        //    {
        //        return ValidationProblem(e.Message);
        //    }
        //}

        //[HttpPatch]
        //[Route("{customerId}")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<Customer> PatchCustomer([FromRoute] string customerId, [FromBody] CustomerPatch newCustomer)
        //{
        //    try
        //    {
        //        var customerList = _dao.Customers as IQueryable<Customer>;
        //        var customer = customerList.First(p => p.CustomerId.Equals(customerId));

        //        customer.CustomerFirstName = newCustomer.CustomerFirstName ?? customer.CustomerFirstName;
        //        customer.CustomerLastName = newCustomer.CustomerLastName ?? customer.CustomerLastName;
        //        customer.CustomerPhone = newCustomer.CustomerPhone ?? customer.CustomerPhone;
        //        customer.CustomerEmail = newCustomer.CustomerEmail ?? customer.CustomerEmail;

        //        _dao.Customers.Update(customer);
        //        _dao.SaveChanges();

        //        return new CreatedResult($"/customers/{customer.CustomerId.ToLower()}", customer);
        //    }
        //    catch (Exception e)
        //    {
        //        // Typically an error log is produced here
        //        return ValidationProblem(e.Message);
        //    }
        //}
    }
}
