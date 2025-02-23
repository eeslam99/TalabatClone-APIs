using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.Order_specs;

namespace Talabat.Application.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly IBasketRepository basketRepo;
		private readonly IUnitOfWork unitOfWork;
		private readonly IPaymentService paymentService;

		///private readonly IGenaricRepository<Product> productRepo;
		///private readonly IGenaricRepository<DeliveryMethod> deliveryMethodRepo;
		///private readonly IGenaricRepository<Order> orderRepo;

		public OrderService(
			IBasketRepository basketRepo,
			IUnitOfWork unitOfWork,
			IPaymentService paymentService
			///IGenaricRepository<Product> productRepo,
			///IGenaricRepository<DeliveryMethod> deliveryMethodRepo,
			///IGenaricRepository<Order> orderRepo
			)
		{
			///this.productRepo = productRepo;
			///this.deliveryMethodRepo = deliveryMethodRepo;
			///this.orderRepo = orderRepo;
			this.basketRepo = basketRepo;
			this.unitOfWork = unitOfWork;
			this.paymentService = paymentService;
		}
		public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
		{
			// 1.Get Basket From Baskets Repo

			var basket = await basketRepo.GetBasketAsync(basketId);


			// 2. Get Selected Items at Basket From Products Repo

			var orderItems = new List<OrderItem>();

			if (basket?.Items?.Count > 0)
			{
				foreach (var item in basket.Items)
				{
					var product = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
					var productItemOrdered = new ProductItemOrdered(item.Id,product.Name,product.PictureUrl);

					var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

					orderItems.Add(orderItem);
				}
			}

			// 3. Calculate SubTotal

			var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

			// 4. Get Delivery Method From DeliveryMethods Repo

			var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

			var orderRepo = unitOfWork.Repository<Order>();

			var spec = new OrderWithPaymentIntentSpecification(basket?.PaymentIntentId);

			var existingOrder = await orderRepo.GetWithSpecAsync(spec);

			if (existingOrder != null)
			{
				orderRepo.Delete(existingOrder);
				await paymentService.CreateOrUpdatePaymentIntent(basketId);
			}

			// 5. Create Order 

			var order = new Order(
				buyerEmail: buyerEmail,
				shippingAddress: shippingAddress,
				deliveryMethod: deliveryMethod,
				items: orderItems,
				subtotal: subtotal,
				paymentIntentId: basket?.PaymentIntentId ?? ""
				);

			orderRepo.Add(order);

			// 6. Save To Database [TODO]

			var result = await unitOfWork.CompleteAsync();

			if (result <= 0) return null;

			return order;

		}

		public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
			=> unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

		public Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
		{
			var orderRepo = unitOfWork.Repository<Order>();

			var orderSpec = new OrderSpecifications(orderId, buyerEmail);

			var order = orderRepo.GetWithSpecAsync(orderSpec);
			return order;
		}

		public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			var orderRepo = unitOfWork.Repository<Order>();

			var spec = new OrderSpecifications(buyerEmail);

			var orders = await orderRepo.GetAllWithSpecAsync(spec);

			return orders;
		}
	}
}
