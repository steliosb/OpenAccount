using Service.OpenAccount.Transactions.Core.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.OpenAccount.Transactions.Core.Abstractions
{
	public interface ITransactionManager
	{
		Task Create(Transaction transaction);

		Task<IEnumerable<Transaction>> GetTransactionsByAccountIds(IEnumerable<int> accountIds);
	}
}
