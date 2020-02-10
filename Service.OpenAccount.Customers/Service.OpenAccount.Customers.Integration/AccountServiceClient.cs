using Newtonsoft.Json;
using Serilog;
using Service.OpenAccount.Customers.Integration.Abstractions;
using Service.OpenAccount.Customers.Integration.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.OpenAccount.Customers.Integration
{
	public class AccountServiceClient : IAccountServiceClient
	{
		private readonly IAccountServiceClientConfig _config;
		private static readonly HttpClient _httpClient = new HttpClient();
		public AccountServiceClient(IAccountServiceClientConfig config)
		{
			_config = config;
		}

		public async Task<IEnumerable<AccountDetail>> GetByCustomerId(int customerId)
		{
			//Get call in account service
			var response = await _httpClient.GetAsync($"{_config.EndPoint}/getdetailbycustomerid/{customerId}").ConfigureAwait(false);

			HttpContent content = response.Content;
			var jsonResponse = await content.ReadAsStringAsync().ConfigureAwait(false);

			if (response.IsSuccessStatusCode == false)
			{
				throw new Exception($"error in AccountServiceClient {jsonResponse}");
			}

			Log.Information($"Response from account service call get accounts by customer id  {customerId} responded with {jsonResponse}");

			//Deserialize object to json
			var objResponse = JsonConvert.DeserializeObject<List<AccountDetail>>(jsonResponse);

			return objResponse;
		}
	}
}
