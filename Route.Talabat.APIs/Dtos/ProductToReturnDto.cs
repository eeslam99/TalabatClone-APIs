using Talabat.Core.Entites;

namespace Route.Talabat.APIs.Dtos
{
	public class ProductToReturnDto
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public decimal Price { get; set; }
		public int BrandId { get; set; } // foregin key column => productBrand

		public string Brand { get; set; } // Navigational prop one 

		public int CategoryId { get; set; } // foregin key column => ProductCategory
		public string Category { get; set; } // Navigational prop one 
	}
}
