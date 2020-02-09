using AutoMapper;
using Service.OpenAccount.Customers.Core.Abstractions;
using Service.OpenAccount.Customers.Core.Abstractions.Models;
using Service.OpenAccount.Customers.Data.Abstractions;
using Service.OpenAccount.Customers.Integration.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.OpenAccount.Customers.Core
{
	public class CustomerManager : ICustomerManager
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IAccountServiceClient _accountClient;
		private readonly IMapper _mapper;
		public CustomerManager(ICustomerRepository customerRepository, IAccountServiceClient accountClient)
		{
			_customerRepository = customerRepository;
			_accountClient = accountClient;
			_mapper = new MappingConfiguration().GetConfigureMapper();
		}

		public async Task<Customer> GetById(int customerId)
		{
			try
			{
				//Get customer from DB
				var customerDto = await _customerRepository.GetById(customerId).ConfigureAwait(false);

				//Map the customer object from DB to customer core object
				var customer = _mapper.Map<Customer>(customerDto);

				return customer;
			}
			catch (Exception)
			{
				//TODO: log
				throw;
			}
		}

		/// <summary>
		/// Get customer's detail from customer id
		/// </summary>
		/// <param name="customerId"></param>
		/// <returns></returns>
		public async Task<CustomerDetail> GetDetail(int customerId)
		{
			try
			{
				//Get customer from DB
				var customerDto = await _customerRepository.GetById(customerId).ConfigureAwait(false);

				if (customerDto == null) return null;

				//Map customer detail object from DB to customer detail core object
				var customerDetail = _mapper.Map<CustomerDetail>(customerDto);

				//Call account service in order to collect account details by customer id
				var accountDetailsIntegration = await _accountClient.GetByCustomerId(customerId).ConfigureAwait(false);

	
				customerDetail.Accounts = _mapper.Map<List<AccountDetail>>(accountDetailsIntegration);

				//Compute the balance of customer(Sum of all transactions amount)
				customerDetail.Balance = customerDetail.Accounts.Sum(a => a.Transactions.Sum(t => t.Amount));

				return customerDetail;
			}
			catch(Exception)
			{
				//TODO: log
				throw;
			}
		}
	}
}
