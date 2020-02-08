using Service.OpenAccount.Accounts.Integration.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.OpenAccount.Accounts.Integration.Abstractions
{
	public interface ICustomerServiceClient
	{
		Task<Customer> GetById(int customerId);
	}
}
