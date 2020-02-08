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
		public static List<TransactionDto> _transactions = new List<TransactionDto>();

		public async Task<IEnumerable<TransactionDto>> GetTransactionsByAccountIds(IEnumerable<int> accountIds)
		{
			return _transactions.Where(t => accountIds.Contains(t.AccountId));
		}

		public async Task Create(TransactionDto transaction)
		{
			transaction.Id = _transactions.Count() + 1;
			_transactions.Add(transaction);
		}
	}
}
