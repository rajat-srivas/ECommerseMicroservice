using ECommerse.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Search.Interfaces
{
	public interface IOrderService
	{
		Task<(bool isSuccess, IEnumerable<Order> Orders, string ErrorMessage)>	GetOrderAsync(int customerId);
	}
}
