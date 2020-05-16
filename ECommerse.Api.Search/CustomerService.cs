using ECommerse.Api.Search.Interfaces;
using ECommerse.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerse.Api.Search
{
	public class CustomerService : ICustomerService
	{
		private readonly IHttpClientFactory httpClientFactory;
		private readonly ILogger<CustomerService> logger;

		public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
		{
			this.httpClientFactory = httpClientFactory;
			this.logger = logger;
		}

		public async Task<(bool isSuccess, dynamic Cutomers, string ErrorMessage)> GetCustomerAsync(int id)
		{
			try
			{
				var client = httpClientFactory.CreateClient("CustomerService");
				var response = await client.GetAsync($"api/customer/{id}");
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsByteArrayAsync();
					var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
					var result = JsonSerializer.Deserialize<Customer>(content, options);
					return (true, result, null);
				}
				return (false, null, response.ReasonPhrase);
			}
			catch (Exception ex)
			{
				logger?.LogError(ex.ToString());
				return (false, null, ex.Message);
			}
		}
	}
}
