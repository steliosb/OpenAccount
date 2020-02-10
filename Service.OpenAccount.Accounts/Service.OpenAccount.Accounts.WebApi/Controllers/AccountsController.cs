using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.OpenAccount.Accounts.Core.Abstractions;
using Service.OpenAccount.Accounts.WebApi.Models;

namespace Service.OpenAccount.Accounts.WebApi.Controllers
{

    /// <summary>
    /// Account controller responsible for new customer's account and transaction creation and fetch accounts details by customer id 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public AccountsController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }


        /// <summary>
        /// Create a new account and transaction if amount is greater than 0 for a customer
        /// </summary>
        /// <response code="200">Operation suceeded</response>
        /// <response code="500">Internal server error</response>       
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Create")]
        public async Task<IActionResult> Create(AccountRequest accountRequest)
        {
            try
            {
                //Request validation
                if (ModelState.IsValid == false) return BadRequest(ModelState);
              
                var account = new Core.Abstractions.Models.Account()
                {
                    CustomerId = accountRequest.CustomerId
                };

                await _accountManager.Create(account, accountRequest.initialCredit).ConfigureAwait(false);

                return Ok(account);
            }
            catch (Exception ex)
            {               
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get accounts details by customer id  
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDetailByCustomerId/{customerId}")]
        public async Task<IActionResult> GetDetailByCustomerId(int customerId)
        {
            try
            {
                //Request validation
                if (ModelState.IsValid == false) return BadRequest(ModelState);

                var accountDetail = await _accountManager.GetDetail(customerId).ConfigureAwait(false);

                return Ok(accountDetail);
            }
            catch (Exception ex)
            {             
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}