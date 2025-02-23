using AutoMapper;
using Route.Talabat.APIs.Dtos;
using Talabat.Core.Entites.Order_Aggregate;

namespace Route.Talabat.APIs.Helpers
{
	public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration configuration;

		public OrderItemPictureUrlResolver(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.Product.PictureUrl))
				return $"{configuration["ApiBaseUrl"]}/{source.Product.PictureUrl}";


			return string.Empty;
		}
	}
}
