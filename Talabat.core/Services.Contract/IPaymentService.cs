using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Core.Services.Contract
{
	public interface IPaymentService
	{
		Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
		
		Task<Order> UpdateOrderStatus(string paymentIntentId, bool isPaid);
	}
}
