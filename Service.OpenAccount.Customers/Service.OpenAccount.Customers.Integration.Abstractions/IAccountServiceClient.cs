using Service.OpenAccount.Customers.Integration.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.OpenAccount.Customers.Integration.Abstractions
{
	public interface IAccountServiceClient
	{
		Task<IEnumerable<AccountDetail>> GetByCustomerId(int customerId);
	}
}
