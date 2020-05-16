using ECommerse.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Customers.Controllers
{
	
	[ApiController]
	[Route("api/customer")]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerProvider customerProvider;

		public CustomerController(ICustomerProvider customerProvider)
		{
			this.customerProvider = customerProvider;
		}

		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			var result = await customerProvider.GetProductAsync();
			if (result.isSuccess)
			{
				return Ok(result.customers);
			}
			return NotFound();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProduct(int id)
		{
			var result = await customerProvider.GetProductByIdAsync(id);
			if (result.isSuccess)
			{
				return Ok(result.customer);
			}
			return NotFound();
		}
	}
}
