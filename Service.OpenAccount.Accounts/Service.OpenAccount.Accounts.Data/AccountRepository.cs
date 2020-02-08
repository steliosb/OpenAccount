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
		public static List<Account> _accounts = new List<Account>();

		public async Task Create(Account account)
		{
			account.Id = _accounts.Count() + 1;
			_accounts.Add(account);
		}

		public async Task<IEnumerable<Account>> GetByCustomerId(int customerId)
		{
			return _accounts.Where(a => a.CustomerId == customerId);
		}
	}
}
