using ECommerse.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Products.Controllers
{
	[ApiController]
	[Route("api/products")]
	public class ProductsController : ControllerBase
	{
		private readonly IProductProvider productsProvider;

		public ProductsController(IProductProvider productsProvider)
		{
			this.productsProvider = productsProvider;
		}

		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			var result = await productsProvider.GetProductAsync();
			if(result.isSuccess)
			{
				return Ok(result.products);
			}
			return NotFound();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProduct(int id)
		{
			var result = await productsProvider.GetProductByIdAsync(id);
			if (result.isSuccess)
			{
				return Ok(result.product);
			}
			return NotFound();
		}
	}
}
