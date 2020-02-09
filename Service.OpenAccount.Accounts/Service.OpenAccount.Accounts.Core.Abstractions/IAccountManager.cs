using Service.OpenAccount.Accounts.Core.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.OpenAccount.Accounts.Core.Abstractions
{
	public interface IAccountManager
	{
		Task Create(Account account, decimal? initialCredit = null);
		Task<IEnumerable<AccountDetail>> GetDetail(int customerId);

	}
}
