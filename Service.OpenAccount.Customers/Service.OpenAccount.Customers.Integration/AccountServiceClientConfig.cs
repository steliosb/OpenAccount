using Service.OpenAccount.Customers.Integration.Abstractions;
using System;

namespace Service.OpenAccount.Customers.Integration
{
	public class AccountServiceClientConfig : IAccountServiceClientConfig
	{
		//Account service endpoint
		public string EndPoint { get; set; }
	}
}
