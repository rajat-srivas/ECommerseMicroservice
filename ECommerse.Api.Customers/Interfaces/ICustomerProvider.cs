using ECommerse.Api.Customers.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Customers.Interfaces
{
	public interface ICustomerProvider
	{
		Task<(bool isSuccess, IEnumerable<Customer> customers, string ErrorMessage)> GetProductAsync();
		Task<(bool isSuccess, Customer customer, string ErrorMessage)> GetProductByIdAsync(int id);
	}
}
