using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using Stripe;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Services.Contract;

namespace Route.Talabat.APIs.Controllers
{
	[Authorize]
	public class PaymentController : BaseApiController
	{
		private readonly IPaymentService paymentService;
		private readonly ILogger<PaymentController> logger;

		// This is your Stripe CLI webhook secret for testing your endpoint locally.
		private const string whSecret = "whsec_a892d1ee8d42d0f0dfddab96645bfb23d5da458a41cd816376e2b6cb63be10bb";

		public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
		{
			this.paymentService = paymentService;
			this.logger = logger;
		}

		[ProducesResponseType(typeof(CustomerBasket),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[HttpPost("{basketid}")] // GEt : /api/payments/{basketid}
		public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
		{
			var basket = await paymentService.CreateOrUpdatePaymentIntent(basketId);
			if (basket == null) return BadRequest(new ApiResponse(400, "An Error With Your Basket"));
			return Ok(basket);
		}


		[HttpPost("webhook")]
		public async Task<IActionResult> WebHook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			
			var stripeEvent = EventUtility.ConstructEvent(json,
				Request.Headers["Stripe-Signature"], whSecret);
			var paymentIntent = (PaymentIntent) stripeEvent.Data.Object;

			Order? order;
			// Handle the event
			switch (stripeEvent.Type)
			{
				case Events.PaymentIntentSucceeded:
					order = await paymentService.UpdateOrderStatus(paymentIntent.Id, true);
					logger.LogInformation("order is succeded ya hamda {0}", order?.PaymentIntentId);
					logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
					break;
				case Events.PaymentIntentPaymentFailed:
					order = await paymentService.UpdateOrderStatus(paymentIntent.Id, false);
					logger.LogInformation("order is Failed ya hamda {0}", order?.PaymentIntentId);

					break;
			}
			
			return Ok();
		}
	}
}
