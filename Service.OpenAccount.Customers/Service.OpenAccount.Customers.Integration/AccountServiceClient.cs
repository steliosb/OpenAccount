﻿using Newtonsoft.Json;
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

			var response = await _httpClient.GetAsync($"{_config.EndPoint}/getdetailbycustomerid/{customerId}").ConfigureAwait(false);

			HttpContent content = response.Content;
			var jsonResponse = await content.ReadAsStringAsync().ConfigureAwait(false);

			if (response.IsSuccessStatusCode == false)
			{
				throw new Exception($"error in AccountServiceClient {jsonResponse}");
			}

			var objResponse = JsonConvert.DeserializeObject<List<AccountDetail>>(jsonResponse);

			return objResponse;
		}
	}
}