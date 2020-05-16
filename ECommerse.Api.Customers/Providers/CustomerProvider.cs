using ECommerse.Api.Customers.DB;
using ECommerse.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Customers.Providers
{
	public class CustomerProvider : ICustomerProvider
	{
		private readonly CustomerDbContext dbContext;
		private readonly ILogger<CustomerProvider> logger;

		public CustomerProvider(CustomerDbContext dbContext, ILogger<CustomerProvider> logger)
		{
			this.dbContext = dbContext;
			this.logger = logger;
			SeedData();
		}

		private void SeedData()
		{
			if(!dbContext.Customers.Any())
			{ 
				dbContext.Customers.Add(new DB.Customer() { Id = 1, Name = "John", Address = "Noida"});
				dbContext.Customers.Add(new DB.Customer() { Id = 2, Name = "Jonny", Address = "London"  });
				dbContext.Customers.Add(new DB.Customer() { Id = 3, Name = "Jimmy", Address = "Paris" });
				dbContext.Customers.Add(new DB.Customer() { Id = 4, Name = "James", Address = "Dublin"  });
				dbContext.SaveChanges();
			}
		}

		public async Task<(bool isSuccess, IEnumerable<Customer> customers, string ErrorMessage)> GetProductAsync()
		{
			try
			{
				var orders = await dbContext.Customers.ToListAsync();
				if (orders != null && orders.Any())
				{
					return (true, orders, null);
				}

				return (false, null, "Not Found");
			}
			catch (Exception ex)
			{
				logger?.LogError(ex.ToString());
				return (false, null, ex.Message);
			}
		}

		public async Task<(bool isSuccess, Customer customer, string ErrorMessage)> GetProductByIdAsync(int id)
		{
			try
			{
				var order = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
				if (order != null)
				{
					return (true, order, null);
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
