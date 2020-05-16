using AutoMapper;
using ECommerse.Api.Products.DB;
using ECommerse.Api.Products.Interfaces;
using ECommerse.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Products.Providers
{
	public class ProductProvider : IProductProvider
	{
		private readonly ProductDbContext dbContext;
		private readonly ILogger<ProductProvider> logger;
		private readonly IMapper mapper;

		public ProductProvider(ProductDbContext dbContext, ILogger<ProductProvider> logger, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.logger = logger;
			this.mapper = mapper;

			SeedData();
		}

		private void SeedData()
		{
			if(!dbContext.Products.Any())
			{
				dbContext.Products.Add(new DB.Product() { Id = 1, Name = "Keyboard", Price = 100, Inventory = 10 });
				dbContext.Products.Add(new DB.Product() { Id = 2, Name = "Headphones", Price = 700, Inventory = 10 });
				dbContext.Products.Add(new DB.Product() { Id = 3, Name = "Monitor", Price = 11100, Inventory = 10 });
				dbContext.Products.Add(new DB.Product() { Id = 4, Name = "CPU", Price = 20000, Inventory = 10 });
				dbContext.SaveChanges();
			}
		}

		public async Task<(bool isSuccess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductAsync()
		{
			try
			{
				var products = await dbContext.Products.ToListAsync();
				if(products != null && products.Any())
				{
					var result = mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
					return (true, result, null);
				}

				return (false, null, "Not Found");
			}
			catch(Exception ex)
			{
				logger?.LogError(ex.ToString());
				return (false, null, ex.Message);
			}
		}

		public async Task<(bool isSuccess, Models.Product product, string ErrorMessage)> GetProductByIdAsync(int id)
		{
			try
			{
				var product = await dbContext.Products.FirstOrDefaultAsync(x=>x.Id == id);
				if (product != null)
				{
					var result = mapper.Map<DB.Product, Models.Product>(product);
					return (true, result, null);
				}

				return (false, null, "Not Found");
			}
			catch (Exception ex)
			{
				logger?.LogError(ex.ToString());
				return (false, null, ex.Message);
			}
		}
	}
}
