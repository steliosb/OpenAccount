using System;
using System.Collections.Generic;
using System.Text;

namespace Service.OpenAccount.Accounts.Integration.Abstractions
{
	public interface ICustomerServiceClientConfig
	{
		string Endpoint { get; set; }
	}
}
