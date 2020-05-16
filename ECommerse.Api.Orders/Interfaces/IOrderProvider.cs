using ECommerse.Api.Customers.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Customers.Interfaces
{
	public interface IOrderProvider
	{
		Task<(bool isSuccess, IEnumerable<Order> orders, string ErrorMessage)> GetOrderAsync();
		Task<(bool isSuccess, IEnumerable<Order> orders, string ErrorMessage)> GetOrderByCustomerAsync(int customerId);
	}
}
