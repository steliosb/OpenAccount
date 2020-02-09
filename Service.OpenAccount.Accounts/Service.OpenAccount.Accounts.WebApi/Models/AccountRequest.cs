using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service.OpenAccount.Accounts.WebApi.Models
{
	/// <summary>
	/// Account Request
	/// </summary>
	public class AccountRequest
	{
		/// <summary>
		/// Customer Id
		/// </summary>
		[Required]
		public int CustomerId { get; set; }
		/// <summary>
		/// Initial Credit of transaction
		/// </summary>
		public decimal? initialCredit { get; set; }
	}
}
