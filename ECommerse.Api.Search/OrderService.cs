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
	public class OrderService : IOrderService
	{
		private readonly IHttpClientFactory httpClientFactory;
		private readonly ILogger<OrderService> logger;

		public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
		{
			this.httpClientFactory = httpClientFactory;
			this.logger = logger;
		}
		public async Task<(bool isSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrderAsync(int customerId)
		{
			try
			{
				var client = httpClientFactory.CreateClient("OrderService");
				var response = await client.GetAsync($"api/order/{customerId}");
				if(response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsByteArrayAsync();
					var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
					var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
					return (true, result, null);
				}
				return (false, null, response.ReasonPhrase);

			}
			catch(Exception ex)
			{
				logger.LogError(ex.ToString());
				return (false, null, ex.Message);
			}
		}
	}
}
