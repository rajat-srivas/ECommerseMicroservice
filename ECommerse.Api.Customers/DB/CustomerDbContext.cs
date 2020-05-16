﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Customers.DB
{
	public class CustomerDbContext: DbContext
	{
		public DbSet<Customer> Customers { get; set; }
		public CustomerDbContext(DbContextOptions options) : base(options)
		{

		}
	}
}
