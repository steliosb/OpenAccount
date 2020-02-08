using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service.OpenAccount.Transactions.WebApi.Models
{
	public class Transaction
	{
		public int Id { get; set; }
		[Required]
		public int AccountId { get; set; }
		[Required]
		public decimal Amount { get; set; }
	}
}
