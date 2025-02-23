using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites
{
	public class Product : BaseEntity
	{
        public string Name { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; } // foregin key column => productBrand

        public ProductBrand	Brand { get; set; } // Navigational prop one 

		public int CategoryId { get; set; } // foregin key column => ProductCategory
		public ProductCategory Category { get; set; } // Navigational prop one 
	}
}
