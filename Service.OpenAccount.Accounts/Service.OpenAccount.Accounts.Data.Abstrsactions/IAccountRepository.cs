using Service.OpenAccount.Accounts.Data.Abstrsactions.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.OpenAccount.Accounts.Data.Abstrsactions
{
	public interface IAccountRepository
	{
		Task Create(AccountDto account);

		Task<IEnumerable<AccountDto>> GetByCustomerId(int customerId);
	}
}
