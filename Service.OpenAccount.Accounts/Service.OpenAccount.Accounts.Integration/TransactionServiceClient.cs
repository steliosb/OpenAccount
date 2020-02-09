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
	public class TransactionServiceClient : ITransactionServiceClient
	{
		private readonly ITransactionServiceClientConfig _config;
		private static readonly HttpClient _httpClient = new HttpClient();
		public TransactionServiceClient(ITransactionServiceClientConfig config)
		{
			_config = config;
		}

		public async Task Create(Transaction transaction)
		{
			var request = JsonConvert.SerializeObject(transaction);

			//Post call to transaction service in order to create transaction
			var response = await _httpClient.PostAsync($"{_config.Endpoint}/create", new StringContent(request, Encoding.UTF8, "application/json")).ConfigureAwait(false);

			HttpContent content = response.Content;
			var jsonResponse = await content.ReadAsStringAsync().ConfigureAwait(false);

			if(response.IsSuccessStatusCode == false)
			{
				throw new Exception($"error in TransactionServiceClient {jsonResponse}");
			}

			var objResponse = JsonConvert.DeserializeObject<Transaction>(jsonResponse);

			transaction.Id = objResponse.Id;
		}

		public async Task<IEnumerable<Transaction>> GetByAccountIds(IEnumerable<int> accountIds)
		{
			var request = JsonConvert.SerializeObject(accountIds);

			//Post call to transaction service in order get transaction details about customer's accounts
			var response = await _httpClient.PostAsync($"{_config.Endpoint}/getbyaccountids", new StringContent(request, Encoding.UTF8, "application/json")).ConfigureAwait(false);

			HttpContent content = response.Content;
			var jsonResponse = await content.ReadAsStringAsync().ConfigureAwait(false);

			if (response.IsSuccessStatusCode == false)
			{
				throw new Exception($"error in TransactionServiceClient {jsonResponse}");
			}

			var objResponse = JsonConvert.DeserializeObject<List<Transaction>>(jsonResponse);

			return objResponse;

		}
	}
}
