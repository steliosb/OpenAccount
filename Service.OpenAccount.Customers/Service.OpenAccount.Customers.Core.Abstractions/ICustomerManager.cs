using Service.OpenAccount.Customers.Core.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace Service.OpenAccount.Customers.Core.Abstractions
{
	public interface ICustomerManager
	{
		Task<CustomerDetail> GetDetail(int customerId);
		Task<Customer> GetById(int customerId);
	}
}
