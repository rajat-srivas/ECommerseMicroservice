using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerse.Api.Products.Profile
{
	public class ProductProfile: AutoMapper.Profile
	{
		public ProductProfile()
		{
			CreateMap<DB.Product, Models.Product>();
		}

	}
}
