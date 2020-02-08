using System;
using System.Collections.Generic;
using System.Text;

namespace Service.OpenAccount.Accounts.Core.Abstractions.Models
{
	public class AccountDetail : Account
	{
		public IEnumerable<Transaction> Transactions { set; get; }
	}
}
