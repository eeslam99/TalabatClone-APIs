using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Dtos;
using Route.Talabat.APIs.Errors;
using System.Security.Claims;
using Talabat.Application.AuthService;
using Talabat.Application.OrderService;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Services.Contract;

namespace Route.Talabat.APIs.Controllers
{

	public class OrdersController : BaseApiController
	{
		private readonly IOrderService orderService;
		private readonly IMapper mapper;

		public OrdersController(
			IOrderService orderService,
			IMapper mapper)
        {
			this.orderService = orderService;
			this.mapper = mapper;
		}
		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[HttpPost] // POST /api/Orders
		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
		{
			var address = mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
			var order = await orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);

			if (order == null) return BadRequest(new ApiResponse(400));

			return Ok(mapper.Map<Order, OrderToReturnDto>(order));
		}


		[HttpGet] // GET : /api/Orders
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser(string email)
		{
			var orders = await orderService.GetOrdersForUserAsync(email);
			return Ok(mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
		}


		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")] // GET : /api/Orders/1
		public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id, string email)
		{
			var order = await orderService.GetOrderByIdForUserAsync(id, email);

			if (order == null) return NotFound(new ApiResponse(404));

			return Ok(mapper.Map<OrderToReturnDto>(order));
		}

		[Authorize]
		[HttpGet("deliveryMethods")] // GEt : /api/Orders/deliveryMethods
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
		{
			var deliveryMethods = await orderService.GetDeliveryMethodsAsync();
			return Ok(deliveryMethods);
		}


	}
}
