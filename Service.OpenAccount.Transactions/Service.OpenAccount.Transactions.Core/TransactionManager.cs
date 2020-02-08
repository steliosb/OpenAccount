using AutoMapper;
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
				var transactionDto = _mapper.Map<TransactionDto>(transaction);
				await _transactionRepository.Create(transactionDto).ConfigureAwait(false);
				_mapper.Map<TransactionDto, Transaction>(transactionDto, transaction);
			}
			catch(Exception ex)
			{
				//TODO: log
				throw;
			}
		}

		public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIds(IEnumerable<int> accountIds)
		{
			try
			{
				var transactionDtos = await _transactionRepository.GetTransactionsByAccountIds(accountIds).ConfigureAwait(false);
				var transactions =_mapper.Map<List<Transaction>>(transactionDtos);
				return transactions;
			}
			catch (Exception ex)
			{
				//TODO: log
				throw;
			}
		}
	}
}
