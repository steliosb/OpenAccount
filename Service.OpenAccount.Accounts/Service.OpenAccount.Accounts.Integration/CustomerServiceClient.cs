using Newtonsoft.Json;
using Service.OpenAccount.Accounts.Integration.Abstractions;
using Service.OpenAccount.Accounts.Integration.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.OpenAccount.Accounts.Integration
{
	public class CustomerServiceClient : ICustomerServiceClient
	{
		private readonly ICustomerServiceClientConfig _config;
		private static readonly HttpClient _httpClient = new HttpClient();
		public CustomerServiceClient(ICustomerServiceClientConfig config)
		{
			_config = config;
		}
		public async Task<Customer> GetById(int customerId)
		{
			//Get call to customer service in order to take the customer
			var response = await _httpClient.GetAsync($"{_config.Endpoint}/{customerId}").ConfigureAwait(false);

			HttpContent content = response.Content;
			var jsonResponse = await content.ReadAsStringAsync().ConfigureAwait(false);

			if (response.IsSuccessStatusCode == false)
			{
				if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
				throw new Exception($"error in CustomerServiceClient {jsonResponse}");
			}

			var objResponse = JsonConvert.DeserializeObject<Customer>(jsonResponse);

			return objResponse;

		}
	}
}
