using AutoMapper;
using Route.Talabat.APIs.Dtos;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Identity;
using OrderModule =  Talabat.Core.Entites.Order_Aggregate;

namespace Route.Talabat.APIs.Helpers
{
	public class MappingProfiles : Profile
	{

		public MappingProfiles()
		{
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
				.ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
				.ForMember(p => p.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

			CreateMap<CustomerBasketDto, CustomerBasket>();
			CreateMap<BasketItemDto, BasketItem>();
			CreateMap<Address, AddressDto>().ReverseMap();

			CreateMap<AddressDto, OrderModule.Address>();

			CreateMap<OrderModule.Order, OrderToReturnDto>()
				.ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
				.ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));

			CreateMap<OrderModule.OrderItem, OrderItemDto>()
				.ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
				.ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
				.ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
				.ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());

		}
	}
}
