using System;
using System.Collections.Generic;
using System.Text;

namespace Service.OpenAccount.Accounts.Core.Abstractions.Models
{
	public class Transaction
	{
		public int Id { get; set; }
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
	}
}
