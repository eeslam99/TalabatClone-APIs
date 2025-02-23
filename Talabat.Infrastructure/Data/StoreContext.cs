using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Infrastructure.Data.Config;

namespace Talabat.Infrastructure.Data
{
	public class StoreContext : DbContext
	{
		private readonly DbContextOptions<StoreContext> _options;

		public StoreContext(DbContextOptions<StoreContext> options) : base(options)
		{
			_options = options;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfiguration( new ProductConfigurations() );

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> ProductBrands { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
	}
}
