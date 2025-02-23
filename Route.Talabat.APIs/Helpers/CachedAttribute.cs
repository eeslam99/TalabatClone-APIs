using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Services.Contract;

namespace Route.Talabat.APIs.Helpers
{
	public class CachedAttribute : Attribute, IAsyncActionFilter
	{
		private readonly int timeToliveInSecounds;

		public CachedAttribute(int timeToliveInSecounds)
		{
			this.timeToliveInSecounds = timeToliveInSecounds;
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var responseCacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
			// ask clr for creating object from "ResponseCacheService" Explicitly 

			var cachekey = GenerateCachekeyFromRequest(context.HttpContext.Request);

			var response = await responseCacheService.GetCachedResponseAsync(cachekey);

			if (!string.IsNullOrEmpty(response))
			{
				var result = new ContentResult()
				{
					Content = response,
					ContentType = "application/json",
					StatusCode = 200,
				};

				context.Result = result;
				return;
			} // response is not cached 

			var executedActionContext = await next.Invoke(); // will execute the next acion filter or the action itself 
            
			if (executedActionContext.Result is OkObjectResult okObjectResult && okObjectResult.Value is not null)
			{
				await responseCacheService.CacheResponseAsync(cachekey, okObjectResult.Value, TimeSpan.FromSeconds(timeToliveInSecounds));
			}

			
		}

		private string GenerateCachekeyFromRequest(HttpRequest request)
		{
			var keyBuilder = new StringBuilder();

			keyBuilder.Append(request.Path);

			foreach (var (key,value) in request.Query.OrderBy(x => x.Key))
			{
				keyBuilder.Append($"|{key}-{value}");
			}
			return keyBuilder.ToString();
		}
	}
}
