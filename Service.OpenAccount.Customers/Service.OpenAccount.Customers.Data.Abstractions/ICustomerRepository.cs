using Service.OpenAccount.Customers.Data.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace Service.OpenAccount.Customers.Data.Abstractions
{
	public interface ICustomerRepository
	{
		Task<Customer> GetById(int id);

	}
}
