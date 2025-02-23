using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Dtos;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helpers;
using Talabat.Core.Entites;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_specs;

namespace Route.Talabat.APIs.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IMapper mapper;
		private readonly IproductService productService;
		////private readonly IGenaricRepository<Product> productsRepo;
		////private readonly IGenaricRepository<ProductBrand> brandsRepo;
		////private readonly IGenaricRepository<ProductCategory> categoriesRepo;

		public ProductsController(
			//IGenaricRepository<Product> productsRepo,
			//IGenaricRepository<ProductBrand> brandsRepo,
			//IGenaricRepository<ProductCategory> categoriesRepo
			IMapper mapper,
			IproductService productService
			)
        {
			this.mapper = mapper;
			this.productService = productService;
			///this.productsRepo = productsRepo;
			///this.brandsRepo = brandsRepo;
			///this.categoriesRepo = categoriesRepo;
		}
		[CachedAttribute(600)]
		// GET /api/products
		//[Authorize]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
		{
			var products = await productService.GetProductsAsync(specParams);

			var count = await productService.GetCountAsync(specParams);

			///JsonResult result = new JsonResult(products);
			///OkResult result = new OkResult(products);

			var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);



			return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex,specParams.PageSize,count, data));
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{

			var product = await productService.GetProductAsync(id);

			if (product is null)
			{
				return NotFound(new ApiResponse(404)); // 404 
			}

			return Ok(mapper.Map<Product,ProductToReturnDto>(product)); // 200 
		}

		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await productService.GetBrandsAsync();
			return Ok(brands);
		}

		[HttpGet("categories")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
		{
			var categories = await productService.GetCategoriesAsync();
			return Ok(categories);
		}

    }
}
