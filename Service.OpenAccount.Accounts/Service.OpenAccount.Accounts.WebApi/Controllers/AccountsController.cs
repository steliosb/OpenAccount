using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.OpenAccount.Accounts.Core.Abstractions;
using Service.OpenAccount.Accounts.WebApi.Models;

namespace Service.OpenAccount.Accounts.WebApi.Controllers
{
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
        /// Create a new account for a customer
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
                if (ModelState.IsValid == false) return BadRequest(ModelState);

                var account = new Core.Abstractions.Models.Account()
                {
                    CustomerId = accountRequest.CustomerId
                };

                await _accountManager.Create(account, accountRequest.InitialAmount).ConfigureAwait(false);

                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpGet]
        [Route("GetDetailByCustomerId/{customerId}")]
        public async Task<IActionResult> GetDetailByCustomerId(int customerId)
        {
            try
            {
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