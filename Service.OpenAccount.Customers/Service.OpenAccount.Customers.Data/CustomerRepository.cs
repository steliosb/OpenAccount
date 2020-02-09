using Service.OpenAccount.Customers.Data.Abstractions;
using Service.OpenAccount.Customers.Data.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace Service.OpenAccount.Customers.Data
{
	public class CustomerRepository : ICustomerRepository
	{
		public async Task<Customer> GetById(int id)
		{
			return await Task.Run(()=>GetByIdAsync(id));

		}

		public Customer GetByIdAsync(int id)
		{
			//we have the first 1000 customers in our DB
			if (id > 0 && id <= 1000)
			{
				return new Customer()
				{
					Id = id,
					Name = $"FirstName-{id}",
					Surname = $"LastName-{id}"
				};
			}
			else
			{
				return  null;
			}
		}
	}
}
