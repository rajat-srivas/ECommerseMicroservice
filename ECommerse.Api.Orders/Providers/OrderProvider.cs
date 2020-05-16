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
	public class OrderProvider : IOrderProvider
	{
		private readonly OrderDbContext dbContext;
		private readonly ILogger<OrderProvider> logger;

		public OrderProvider(OrderDbContext dbContext, ILogger<OrderProvider> logger)
		{
			this.dbContext = dbContext;
			this.logger = logger;
            SeedData();
		}

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                dbContext.Orders.Add(new Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                dbContext.Orders.Add(new Order()
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Order> orders, string ErrorMessage)> GetOrderAsync()
		{
			try
			{
				var orders = await dbContext.Orders.ToListAsync();
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

		public async Task<(bool isSuccess, IEnumerable<Order> orders, string ErrorMessage)> GetOrderByCustomerAsync(int customerId)
		{
			try
			{
                var orders = await dbContext.Orders
                    .Where(o => o.CustomerId == customerId)
                    .Include(o => o.Items)
                    .ToListAsync();
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
	}
}
