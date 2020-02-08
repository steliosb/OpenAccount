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
				var customerDto = await _customerRepository.GetById(customerId).ConfigureAwait(false);
				var customer = _mapper.Map<Customer>(customerDto);

				return customer;
			}
			catch (Exception ex)
			{
				//TODO: log
				throw;
			}
		}

		public async Task<CustomerDetail> GetDetail(int customerId)
		{
			try
			{
				var customerDto = await _customerRepository.GetById(customerId).ConfigureAwait(false);

				if (customerDto == null) return null;
				
				var customerDetail = _mapper.Map<CustomerDetail>(customerDto);

				var accountDetailsIntegration = await _accountClient.GetByCustomerId(customerId).ConfigureAwait(false);

				customerDetail.Accounts = _mapper.Map<List<AccountDetail>>(accountDetailsIntegration);

				customerDetail.Balance = customerDetail.Accounts.Sum(a => a.Transactions.Sum(t => t.Amount));

				return customerDetail;
			}
			catch(Exception ex)
			{
				//TODO: log
				throw;
			}
		}
	}
}
