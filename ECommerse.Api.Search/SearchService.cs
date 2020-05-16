using ECommerse.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Search
{
	public class SearchService : ISearchService
	{
		private readonly IOrderService orderService;
		private readonly IProductService productService;
		private readonly ICustomerService customerService;

		public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
		{
			this.orderService = orderService;
			this.productService = productService;
			this.customerService = customerService;
		}
		public async Task<(bool isSuccess, dynamic SearchResult)> SearchAsync(int customerId)
		{
			var orders = await orderService.GetOrderAsync(customerId);
			var productResults = await productService.GetProductsAsync();
			var customerResults = await customerService.GetCustomerAsync(customerId);
			if(orders.isSuccess)
			{
				foreach(var order in orders.Orders)
				{
					foreach(var item in order.Items)
					{
						item.ProductName = productResults.isSuccess ? 
										   productResults.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name
							: "Product info not available";
					}
				}
				var result = new
				{
					Customer = customerResults.isSuccess ?
								customerResults.Cutomers : new { Name = "Customer info not available" },
					Orders = orders.Orders
				};
				return (true, result);
			}

			return (false, null);
		}
	}
}
