using System;
using System.Collections.Generic;
using System.Text;

namespace Service.OpenAccount.Customers.Integration.Abstractions
{
	public interface IAccountServiceClientConfig
	{
		string EndPoint { get; set; }
	}
}
