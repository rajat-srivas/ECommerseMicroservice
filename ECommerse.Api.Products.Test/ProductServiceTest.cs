using AutoMapper;
using ECommerse.Api.Products.DB;
using ECommerse.Api.Products.Profile;
using ECommerse.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerse.Api.Products.Test
{
	public class ProductServiceTest
	{
		[Fact]
		public async Task GetProductReturnsAllProducsts()
		{
			var options = new DbContextOptionsBuilder<ProductDbContext>()
						.UseInMemoryDatabase(nameof(GetProductReturnsAllProducsts))
						.Options;
			var dbContext = new ProductDbContext(options);
			CreateProducts(dbContext);

			var productProfile = new ProductProfile();
			var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
			var mapper = new Mapper(configuration);

			var productsProvider = new ProductProvider(dbContext, null, mapper);

			var product = await productsProvider.GetProductAsync();

			Assert.True(product.isSuccess);
			Assert.True(product.products.Any());
			Assert.Null(product.ErrorMessage);
		}

		[Fact]
		public async Task GetProductReturnsProductUsingValidProductId()
		{
			var options = new DbContextOptionsBuilder<ProductDbContext>()
						.UseInMemoryDatabase(nameof(GetProductReturnsAllProducsts))
						.Options;
			var dbContext = new ProductDbContext(options);
			CreateProducts(dbContext);

			var productProfile = new ProductProfile();
			var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
			var mapper = new Mapper(configuration);

			var productsProvider = new ProductProvider(dbContext, null, mapper);

			var product = await productsProvider.GetProductByIdAsync(1);

			Assert.True(product.isSuccess);
			Assert.NotNull(product.product);
			Assert.True(product.product.Id == 1);
			Assert.Null(product.ErrorMessage);
		}

		[Fact]
		public async Task GetProductReturnsProductUsingInValidProductId()
		{
			var options = new DbContextOptionsBuilder<ProductDbContext>()
						.UseInMemoryDatabase(nameof(GetProductReturnsAllProducsts))
						.Options;
			var dbContext = new ProductDbContext(options);
			CreateProducts(dbContext);

			var productProfile = new ProductProfile();
			var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
			var mapper = new Mapper(configuration);

			var productsProvider = new ProductProvider(dbContext, null, mapper);

			var product = await productsProvider.GetProductByIdAsync(-1);

			Assert.False(product.isSuccess);
			Assert.Null(product.product);
			Assert.NotNull(product.ErrorMessage);
		}

		private void CreateProducts(ProductDbContext dbContext)
		{
			for(int i = 1; i<=10; i++)
			{
				dbContext.Products.Add(new Product()
				{
					Id = i,
					Name = Guid.NewGuid().ToString(),
					Inventory = i + 10,
					Price = (decimal)(i * 31.3)
				});
				dbContext.SaveChanges();
				
			}
		}
	}
}
