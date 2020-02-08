using Service.OpenAccount.Accounts.Integration.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.OpenAccount.Accounts.Integration
{
	public class CustomerServiceClientConfig : ICustomerServiceClientConfig
	{
		public string Endpoint { get ; set; }
	}
}
