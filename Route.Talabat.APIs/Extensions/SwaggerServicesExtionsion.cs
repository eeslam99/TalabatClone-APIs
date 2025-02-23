using Microsoft.AspNetCore.Builder;

namespace Route.Talabat.APIs.Extensions
{
	public static class SwaggerServicesExtionsion
	{
		public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			return services;
		}

		public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();

			return app;
		}
	}
}
