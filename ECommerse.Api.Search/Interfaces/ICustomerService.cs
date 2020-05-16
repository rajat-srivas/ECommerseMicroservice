using ECommerse.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Search.Interfaces
{
	public interface ICustomerService
	{
		Task<(bool isSuccess, dynamic Cutomers, string ErrorMessage)> GetCustomerAsync(int id);
	}

}
