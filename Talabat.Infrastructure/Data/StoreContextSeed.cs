using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Infrastructure.Data
{
	public static class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext dbContext)
		{
			if (! dbContext.ProductBrands.Any())
			{
				var brandData = File.ReadAllText("../Talabat.Infrastructure/Data/Dataseed/brands.json");

				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

				if (brands?.Count > 0)
				{
					foreach (var brand in brands)
					{
						dbContext.Set<ProductBrand>().Add(brand);
					}
					await dbContext.SaveChangesAsync();
				}
			}

			if (!dbContext.ProductCategories.Any())
			{
				var categoriesData = File.ReadAllText("../Talabat.Infrastructure/Data/Dataseed/categories.json");

				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

				if (categories?.Count > 0)
				{
					foreach (var category in categories)
					{
						dbContext.Set<ProductCategory>().Add(category);
					}
					await dbContext.SaveChangesAsync();
				}
			}

			if (!dbContext.Products.Any())
			{
				var productsData = File.ReadAllText("../Talabat.Infrastructure/Data/Dataseed/products.json");

				var products = JsonSerializer.Deserialize<List<Product>>(productsData);

				if (products?.Count > 0)
				{
					foreach (var product in products)
					{
						dbContext.Set<Product>().Add(product);
					}
					await dbContext.SaveChangesAsync();
				}
			}

			if (!dbContext.DeliveryMethods.Any())
			{
				var deliveryMethodsData = File.ReadAllText("../Talabat.Infrastructure/Data/Dataseed/delivery.json");

				var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

				if (deliveryMethods?.Count > 0)
				{
					foreach (var deliveryMethod in deliveryMethods)
					{
						dbContext.Set<DeliveryMethod>().Add(deliveryMethod);
					}
					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
