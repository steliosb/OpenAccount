using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service.OpenAccount.Transactions.WebApi.Models
{
	/// <summary>
	/// Transaction request
	/// </summary>
	public class Transaction
	{
		/// <summary>
		/// Transaction id
		/// Every time customer execute a transaction a new one is created
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Account Id
		/// </summary>
		[Required]
		public int AccountId { get; set; }
		/// <summary>
		/// Ammount of transaction
		/// </summary>
		[Required]
		public decimal Amount { get; set; }
	}
}
