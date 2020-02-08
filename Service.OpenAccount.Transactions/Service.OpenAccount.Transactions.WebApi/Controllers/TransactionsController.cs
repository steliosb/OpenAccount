using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.OpenAccount.Transactions.Core.Abstractions;
using Service.OpenAccount.Transactions.WebApi.Models;

namespace Service.OpenAccount.Transactions.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionManager _transactionManager;
        private readonly IMapper _mapper;
        public TransactionsController(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
            _mapper = new MappingConfiguration().GetConfigureMapper();
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            try
            {
                if (ModelState.IsValid == false) return BadRequest(ModelState);
                
                var transactionCore = _mapper.Map<Core.Abstractions.Models.Transaction>(transaction);
                await _transactionManager.Create(transactionCore).ConfigureAwait(false);
                _mapper.Map(transactionCore, transaction);
                
                return Ok(transaction);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { error= ex.Message });
            }

        }

        [HttpPost]
        [Route("GetByAccountIds")]
        public async Task<IActionResult> GetByAccountIds(List<int> accountIds)
        {
            try
            {
                if (ModelState.IsValid == false) return BadRequest(ModelState);

                var transactionsCore = await _transactionManager.GetTransactionsByAccountIds(accountIds).ConfigureAwait(false);
                var transactions = _mapper.Map<List<WebApi.Models.Transaction>>(transactionsCore);

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }

        }
    }
}