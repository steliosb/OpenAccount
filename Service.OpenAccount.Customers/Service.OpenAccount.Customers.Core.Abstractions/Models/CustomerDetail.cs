using System;
using System.Collections.Generic;
using System.Text;

namespace Service.OpenAccount.Customers.Core.Abstractions.Models
{
	public class CustomerDetail : Customer
	{
		public decimal Balance { set; get; }
		public IEnumerable<AccountDetail> Accounts { set; get; }
	}
}
