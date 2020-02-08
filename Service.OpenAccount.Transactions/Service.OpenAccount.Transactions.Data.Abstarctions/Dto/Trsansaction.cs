using System;
using System.Collections.Generic;
using System.Text;

namespace Service.OpenAccount.Transactions.Data.Abstarctions.Dto
{
	public class TransactionDto
	{
		public int Id { get; set; }
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
	}
}
