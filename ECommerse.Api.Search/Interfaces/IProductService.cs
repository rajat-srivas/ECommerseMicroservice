using ECommerse.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Search.Interfaces
{
	public interface IProductService
	{
		Task<(bool isSuccess, IEnumerable<Products> Products, string ErrorMessage)> GetProductsAsync();
	}
}
