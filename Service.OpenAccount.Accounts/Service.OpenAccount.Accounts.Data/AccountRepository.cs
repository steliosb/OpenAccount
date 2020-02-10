using Service.OpenAccount.Accounts.Data.Abstrsactions;
using Service.OpenAccount.Accounts.Data.Abstrsactions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.OpenAccount.Accounts.Data
{
	public class AccountRepository : IAccountRepository
	{
		public static List<AccountDto> _accounts = new List<AccountDto>();

		public async Task Create(AccountDto account)
		{
			account.Id = _accounts.Count() + 1;
			await Task.Run(() => _accounts.Add(account)).ConfigureAwait(false);
		}

		public async Task<IEnumerable<AccountDto>> GetByCustomerId(int customerId)
		{
			return await Task.Run(()=> _accounts.Where(a => a.CustomerId == customerId)).ConfigureAwait(false);
		}
	}
}
