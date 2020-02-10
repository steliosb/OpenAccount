using AutoMapper;
using Serilog;
using Service.OpenAccount.Transactions.Core.Abstractions;
using Service.OpenAccount.Transactions.Core.Abstractions.Models;
using Service.OpenAccount.Transactions.Data.Abstarctions;
using Service.OpenAccount.Transactions.Data.Abstarctions.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.OpenAccount.Transactions.Core
{
	public class TransactionManager : ITransactionManager
	{
		private readonly ITransactionRepository _transactionRepository;
		private readonly IMapper _mapper;
		public TransactionManager(ITransactionRepository transactionRepository)
		{
			_transactionRepository = transactionRepository;
			_mapper = new MappingConfiguration().GetConfigureMapper();
		}

		public async Task Create(Transaction transaction)
		{
			try
			{
				//Map transaction core object to transaction DB object 
				var transactionDto = _mapper.Map<TransactionDto>(transaction);

				//Call repository in order to create a new transaction for an account
				await _transactionRepository.Create(transactionDto).ConfigureAwait(false);

				//Map the transaction object from DB to transaction core object
				_mapper.Map<TransactionDto, Transaction>(transactionDto, transaction);
			}
			catch(Exception ex)
			{
				Log.Error($"Create transaction with request account id: {transaction.AccountId} and amount: {transaction.Amount} error :{ex.Message}");
				throw;
			}
		}

		public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIds(IEnumerable<int> accountIds)
		{
			try
			{
				//Fetch transactions by account ids
				var transactionDtos = await _transactionRepository.GetTransactionsByAccountIds(accountIds).ConfigureAwait(false);
				var transactions =_mapper.Map<List<Transaction>>(transactionDtos);
				return transactions;
			}
			catch (Exception ex)
			{
				Log.Error($"Response from call transaction service get transaction with account ids {String.Join(",", accountIds)} responded with error: {ex.Message}");
				throw;
			}
		}
	}
}
