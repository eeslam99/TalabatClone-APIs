using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications.Product_specs
{
	public class ProductWithBrandAndCategorySpecifications :BaseSpecifications<Product>
	{
		// this constructor Will be used for creating an Object, that will used to get all products 
		public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams)
			:base(p => 

			     (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search))&&
			     (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value) &&
			     (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value)
			)
		{
			AddIncludes();
			if (!string.IsNullOrEmpty(specParams.Sort))
			{
				switch (specParams.Sort)
				{
					case "priceAsc":
						//OrderBy = p => p.Price;
						AddOrderBy( p => p.Price);
						break;
					case "priceDesc":
						AddOrderByDesc( p => p.Price);
						break;
					default:
						AddOrderBy(p => p.Name);
						break;
				}
			}
			else AddOrderBy(p => p.Name);

			ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
		}

		// this constructor Will be used for creating an Object, that will used to get spicfic product with id  
		public ProductWithBrandAndCategorySpecifications(int id )
			:base(p => p.Id == id)
		{
			AddIncludes();
		}

		private void AddIncludes()
		{
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);
		}
	}
}
