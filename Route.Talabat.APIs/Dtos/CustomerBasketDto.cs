using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.Dtos
{
	public class CustomerBasketDto
	{
		[Required]
		public string Id { get; set; }
		public List<BasketItemDto> Items { get; set; }
	}
}
