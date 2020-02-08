using Service.OpenAccount.Accounts.Data.Abstrsactions.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.OpenAccount.Accounts.Data.Abstrsactions
{
	public interface IAccountRepository
	{
		Task Create(Account account);

		Task<IEnumerable<Account>> GetByCustomerId(int customerId);
	}
}
