using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service.OpenAccount.Accounts.WebApi.Models
{
	public class AccountRequest
	{
		[Required]
		public int CustomerId { get; set; }
		public decimal? InitialAmount { get; set; }
	}
}
