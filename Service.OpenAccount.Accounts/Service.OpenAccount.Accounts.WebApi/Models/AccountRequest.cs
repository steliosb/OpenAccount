using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service.OpenAccount.Accounts.WebApi.Models
{
	/// <summary>
	/// Create new customer account if initial ammount is not 0 using customer id 
	/// </summary>
	public class AccountRequest
	{
		/// <summary>
		/// Customer Id
		/// </summary>
		[Required]
		public int CustomerId { get; set; }
		/// <summary>
		/// Initial Amount of transaction
		/// </summary>
		public decimal? InitialAmount { get; set; }
	}
}
