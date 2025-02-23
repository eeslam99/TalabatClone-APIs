using Route.Talabat.APIs.Errors;
using System.Net;
using System.Text.Json;

namespace Route.Talabat.APIs.Middlewares
{
	// By Convension
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionMiddleware> logger;
		private readonly IWebHostEnvironment env;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
		{
			this.next = next;
			this.logger = logger;
			this.env = env;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				// take and action with the request 
				await next.Invoke(httpContext); // go to the next middleware 


				// take an action with the response 
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message); // development 

				// log exception in (database || files) // production env 

				httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";

				var response = env.IsDevelopment()?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
					:
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

				var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

				var json = JsonSerializer.Serialize(response);  

				await httpContext.Response.WriteAsync(json);
			}
		}
	}
}
