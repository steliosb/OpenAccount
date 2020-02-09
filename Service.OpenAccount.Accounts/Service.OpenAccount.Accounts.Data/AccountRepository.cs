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
			await Task.Run(() => _accounts.Add(new AccountDto()
			{
				Id = _accounts.Count() + 1,
				CustomerId = account.CustomerId
			})).ConfigureAwait(false);
		}

		public async Task<IEnumerable<AccountDto>> GetByCustomerId(int customerId)
		{
			return await Task.Run(()=> _accounts.Where(a => a.CustomerId == customerId)).ConfigureAwait(false);
		}
	}
}
