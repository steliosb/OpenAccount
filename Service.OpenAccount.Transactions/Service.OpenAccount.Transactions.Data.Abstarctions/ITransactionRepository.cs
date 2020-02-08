using Service.OpenAccount.Transactions.Data.Abstarctions.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.OpenAccount.Transactions.Data.Abstarctions
{
	public interface ITransactionRepository
	{
		Task Create(TransactionDto transaction);

		Task<IEnumerable<TransactionDto>> GetTransactionsByAccountIds(IEnumerable<int> accountIds);
	}
}
