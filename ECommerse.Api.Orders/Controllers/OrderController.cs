using ECommerse.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Customers.Controllers
{ 
	[ApiController]
	[Route("api/order")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderProvider orderProvider;

		public OrderController(IOrderProvider orderProvider)
		{
			this.orderProvider = orderProvider;
		}

		[HttpGet]
		public async Task<IActionResult> GetOrders()
		{
			var result = await orderProvider.GetOrderAsync();
			if (result.isSuccess)
			{
				return Ok(result.orders);
			}
			return NotFound();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrder(int id)
		{
			var result = await orderProvider.GetOrderByCustomerAsync(id);
			if (result.isSuccess)
			{
				return Ok(result.orders);
			}
			return NotFound();
		}
	}
}
