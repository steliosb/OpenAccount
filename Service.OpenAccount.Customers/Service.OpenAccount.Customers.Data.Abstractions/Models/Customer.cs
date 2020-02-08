using System;
using System.Collections.Generic;
using System.Text;

namespace Service.OpenAccount.Customers.Data.Abstractions.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
	}
}
