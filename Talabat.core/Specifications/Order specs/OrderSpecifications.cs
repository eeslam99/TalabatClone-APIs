using Talabat.Core.Entites.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.Order_specs
{
	public class OrderSpecifications : BaseSpecifications<Order>
	{
		public OrderSpecifications(string buyerEmail)
			:base(O => O.BuyerEmail == buyerEmail) 
		{
			Includes.Add(o => o.DeliveryMethod);
			Includes.Add(o => o.Items);

			AddOrderByDesc(o => o.OrderDate);

		}

		public OrderSpecifications(int orderId, string buyerEmail)
			: base(o => o.Id == orderId && o.BuyerEmail == buyerEmail)
		{
			Includes.Add(o => o.DeliveryMethod);
			Includes.Add(o => o.Items);

		}

		
	}
}
