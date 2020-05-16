using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Search.Interfaces
{
	public interface ISearchService
	{
		Task<(bool isSuccess, dynamic SearchResult)> SearchAsync(int customerId);
	}
}
