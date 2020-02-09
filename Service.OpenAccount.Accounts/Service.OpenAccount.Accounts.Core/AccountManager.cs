﻿using AutoMapper;
using Service.OpenAccount.Accounts.Core.Abstractions;
using Service.OpenAccount.Accounts.Core.Abstractions.Models;
using Service.OpenAccount.Accounts.Data.Abstrsactions;
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
				var customer = await _customerClient.GetById(account.CustomerId).ConfigureAwait(false);

				if (customer == null) throw new Exception("Customer not found");

				var accountDto = _mapper.Map<Data.Abstrsactions.Dto.Account>(account);
				await _accountRepository.Create(accountDto).ConfigureAwait(false);
				_mapper.Map(accountDto, account);

				if (initialCredit.HasValue && initialCredit.Value != decimal.Zero)
				{
					await _transactionClient.Create(new Integration.Abstractions.Models.Transaction()
					{
						AccountId = account.Id,
						Amount = initialCredit.Value
					}).ConfigureAwait(false);
				}
			}
			catch (Exception ex)
			{
				//TODO: log
				throw;
			}
		}

		public async Task<IEnumerable<AccountDetail>> GetDetail(int customerId)
		{
			try
			{
				var accountDto = await _accountRepository.GetByCustomerId(customerId).ConfigureAwait(false);
				var accountDetails = _mapper.Map<List<AccountDetail>>(accountDto);

				var transactionsIntegration = await _transactionClient.GetByAccountIds(accountDetails.Select(a => a.Id)).ConfigureAwait(false);

				foreach(var accountDetail in accountDetails)
				{
					accountDetail.Transactions = transactionsIntegration.Where(t => t.AccountId == accountDetail.Id).Select(t => _mapper.Map<Transaction>(t));
				}

				return accountDetails;
			}
			catch (Exception ex)
			{
				//TODO: log
				throw;
			}
		}
	}
}
