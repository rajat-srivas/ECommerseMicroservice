using ECommerse.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Products.Interfaces
{
	public interface IProductProvider
	{
		Task<(bool isSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductAsync();
		Task<(bool isSuccess, Product product, string ErrorMessage)> GetProductByIdAsync(int id);
	}
}
