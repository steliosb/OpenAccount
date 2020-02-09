using AutoMapper;
using Service.OpenAccount.Accounts.Core.Abstractions;
using Service.OpenAccount.Accounts.Core.Abstractions.Models;
using Service.OpenAccount.Accounts.Data.Abstrsactions;
using Service.OpenAccount.Accounts.Data.Abstrsactions.Dto;
using Service.OpenAccount.Accounts.Integration.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.OpenAccount.Accounts.Core
{
	public class AccountManager : IAccountManager
	{
		private readonly IAccountRepository _accountRepository;
		private readonly ITransactionServiceClient _transactionClient;
		private readonly ICustomerServiceClient _customerClient;
		private readonly IMapper _mapper;
		public AccountManager(IAccountRepository accountRepository, ITransactionServiceClient transactionClient, ICustomerServiceClient customerClient)
		{
			_accountRepository = accountRepository;
			_transactionClient = transactionClient;
			_customerClient = customerClient;
			_mapper = new MappingConfiguration().GetConfigureMapper();

		}
		public async Task Create(Account account, decimal? initialCredit = null)
		{
			try
			{
				//Call customer service in order to fetch customer details
				var customer = await _customerClient.GetById(account.CustomerId).ConfigureAwait(false);

				//Check existance of customer
				if (customer == null) throw new Exception("Customer not found");

				var accountDto = _mapper.Map<AccountDto>(account);

				//Create account
				await _accountRepository.Create(accountDto).ConfigureAwait(false);
				_mapper.Map(accountDto, account);

				//If amount is not 0 then we are going to create transaction for account
				if (initialCredit.HasValue && initialCredit.Value != decimal.Zero)
				{
					await _transactionClient.Create(new Integration.Abstractions.Models.Transaction()
					{
						AccountId = account.Id,
						Amount = initialCredit.Value
					}).ConfigureAwait(false);
				}
			}
			catch (Exception)
			{
				//TODO: log
				throw;
			}
		}

		public async Task<IEnumerable<AccountDetail>> GetDetail(int customerId)
		{
			try
			{
				//Get customer's account
				var accountDto = await _accountRepository.GetByCustomerId(customerId).ConfigureAwait(false);
				var accountDetails = _mapper.Map<List<AccountDetail>>(accountDto);
				
				//Call transaction service in order to collect transactions by a list of account ids
				var transactionsIntegration = await _transactionClient.GetByAccountIds(accountDetails.Select(a => a.Id)).ConfigureAwait(false);

				//Fill transaction list by account id after integration with transaction service
				foreach(var accountDetail in accountDetails)
				{
					accountDetail.Transactions = transactionsIntegration.Where(t => t.AccountId == accountDetail.Id).Select(t => _mapper.Map<Transaction>(t));
				}

				return accountDetails;
			}
			catch (Exception)
			{
				//TODO: log
				throw;
			}
		}
	}
}
