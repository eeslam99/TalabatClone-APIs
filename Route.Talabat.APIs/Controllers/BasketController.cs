using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Dtos;
using Route.Talabat.APIs.Errors;
using Talabat.Core.Entites;
using Talabat.Core.Repositories.Contract;

namespace Route.Talabat.APIs.Controllers
{
	public class BasketController : BaseApiController
	{
		private readonly IBasketRepository basketRepository;
		private readonly IMapper mapper;

		public BasketController(
			IBasketRepository basketRepository,
			IMapper mapper
			)
        {
			this.basketRepository = basketRepository;
			this.mapper = mapper;
		}

		[HttpGet] // GET: /api/basket?id
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket = await basketRepository.GetBasketAsync(id);
			return Ok(basket ?? new CustomerBasket(id));
		}

		[HttpPost]// POST: /api/basket
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
		{
			var mappedBasket = mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
			var createdOrUpdatedBasket =  await basketRepository.UpdateBasketAsync(mappedBasket);
			if (createdOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
			return Ok(createdOrUpdatedBasket);

		}
		[HttpDelete]// DELETE : /api/basket
		public async Task DeleteBasket(string id)
		{
			await basketRepository.DeleteBasketAsync(id);
		}
    }
}
