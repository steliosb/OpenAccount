using Service.OpenAccount.Transactions.Data.Abstarctions;
using Service.OpenAccount.Transactions.Data.Abstarctions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.OpenAccount.Transactions.Data
{
	public class TransactionRepository : ITransactionRepository
	{
		//Simulation of Transaction table in DB
		public static List<TransactionDto> _transactions = new List<TransactionDto>();

		public async Task<IEnumerable<TransactionDto>> GetTransactionsByAccountIds(IEnumerable<int> accountIds)
		{
			//Return all transactions with account id
			return await Task.Run(()=>_transactions.Where(t => accountIds.Contains(t.AccountId))).ConfigureAwait(false);
		}

		public async Task Create(TransactionDto transaction)
		{
			//Create transaction id
			await Task.Run(() => _transactions.Add(new TransactionDto()
			{
				Id = _transactions.Count() + 1,
				AccountId = transaction.AccountId,
				Amount = transaction.Amount
			})).ConfigureAwait(false);			
		}
	}
}
