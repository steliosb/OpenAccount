using Service.OpenAccount.Accounts.Integration.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.OpenAccount.Accounts.Integration.Abstractions
{
	public interface ITransactionServiceClient
	{
		Task Create(Transaction transaction);
		Task<IEnumerable<Transaction>> GetByAccountIds(IEnumerable<int> accountIds);
	}
}
