using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.OpenAccount.Customers.Core.Abstractions;

namespace Service.OpenAccount.Customers.WebApi.Controllers
{
    /// <summary>
    /// Customer controller for fetch customer data
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        /// <summary>
        /// Fetch customer's detail from customer id
        /// </summary>
        /// <param name="customerId">Send detail request containing customer id</param>
        /// <response code="200">Operation suceeded</response>
        /// <response code="500">Internal server error</response>    
        [HttpGet]
        [Route("Detail/{customerId}")]
        public async Task<IActionResult> Detail(int customerId)
        {
            try
            {
                //Request validation
                if (ModelState.IsValid == false) return BadRequest(ModelState);

                //Get customer details
                var customerDetail = await _customerManager.GetDetail(customerId).ConfigureAwait(false);       
                
                if (customerDetail == null) return NotFound(new { error = $"Customer {customerId} does not exist" });

                return Ok(customerDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("{customerId}")]
        public async Task<IActionResult> Get(int customerId)
        {
            try
            {
                //Request validation
                if (ModelState.IsValid == false) return BadRequest(ModelState);

                //Get customer
                var customer = await _customerManager.GetById(customerId).ConfigureAwait(false);

                if (customer == null) return NotFound(new { error= $"Customer {customerId} does not exist" });

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}