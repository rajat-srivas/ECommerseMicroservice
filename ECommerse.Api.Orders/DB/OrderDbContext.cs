using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Customers.DB
{
	public class OrderDbContext: DbContext
	{
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		public OrderDbContext(DbContextOptions options) : base(options)
		{

		}

	}
}
