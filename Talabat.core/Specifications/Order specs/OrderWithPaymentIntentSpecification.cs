using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Core.Specifications.Order_specs
{
	public class OrderWithPaymentIntentSpecification : BaseSpecifications<Order>
	{
		public OrderWithPaymentIntentSpecification(string paymentIntentId)
			: base(o => o.PaymentIntentId == paymentIntentId)
		{

		}
	}
}
