using System;
using System.Collections.Generic;
using System.Text;

namespace Service.OpenAccount.Customers.Integration.Abstractions.Models
{
	public class AccountDetail
	{
		public int Id { get; set; }

		public int CustomerId { get; set; }
		public IEnumerable<Transaction> Transactions { set; get; }
	}
}
