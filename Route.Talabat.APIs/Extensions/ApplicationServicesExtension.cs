using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helpers;
using System.Text;
using Talabat.Application.AuthService;
using Talabat.Application.CacheService;
using Talabat.Application.OrderService;
using Talabat.Application.PaymentService;
using Talabat.Application.ProductService;
using Talabat.Core;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Infrastructure;

namespace Route.Talabat.APIs.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddSingleton(typeof(IResponseCacheService), typeof(ResponseCacheService));


			services.AddScoped(typeof(IPaymentService), typeof(PaymentService));

			services.AddScoped(typeof(IproductService), typeof(ProductService));


			services.AddScoped(typeof(IOrderService), typeof(OrderService));

			services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            
			//services.AddScoped<IBasketRepository, BasketRepository>(); old way 
			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));// new way with type of 


			///webApplicationBuilder.Services.AddScoped<IGenaricRepository<Product>, GenericRepository<Product>>();
			///old way make u repeate the code 
			/// genaric way if need of type <> gave them of type<>
			///services.AddScoped(typeof(IGenaricRepository<>), typeof(GenericRepository<>));


			//webApplicationBuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
			services.AddAutoMapper(typeof(MappingProfiles));

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
														 .SelectMany(p => p.Value.Errors)
														 .Select(e => e.ErrorMessage)
														 .ToArray();
					var response = new ApiValidationErrorResponse()
					{
						Errors = errors
					};

					return new BadRequestObjectResult(response);
				};
			});

			return services;
		}


		public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/ option =>
			{
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(option =>
				{
					option.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer = configuration["JWT:ValidIssuer"],
						ValidateAudience = true,
						ValidAudience = configuration["JWT:ValidAudience"],
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
						ValidateLifetime = true,
						ClockSkew = TimeSpan.Zero
					};
				});

			services.AddScoped(typeof(IAuthService), typeof(AuthService));

			return services;
		}
	}
}
