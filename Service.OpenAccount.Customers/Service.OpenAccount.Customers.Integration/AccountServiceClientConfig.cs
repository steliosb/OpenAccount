using Service.OpenAccount.Customers.Integration.Abstractions;
using System;

namespace Service.OpenAccount.Customers.Integration
{
	public class AccountServiceClientConfig : IAccountServiceClientConfig
	{
		public string EndPoint { get; set; }
	}
}
