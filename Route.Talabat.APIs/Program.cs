
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Extensions;
using Route.Talabat.APIs.Helpers;
using Route.Talabat.APIs.Middlewares;
using StackExchange.Redis;
using System.Text;
using Talabat.Application.AuthService;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Infrastructure;
using Talabat.Infrastructure.Data;
using Talabat.Infrastructure.Identity;

namespace Route.Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			

			/// builder is the web application builder that will create the app and the privios name
			/// was builder and the new name will be webApplicationBuilder
			/// and here we are creating the app by call  WebApplication.CreateBuilder(args);
			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region Configure Service method from dot net 5 
			// Add services to the container.

			webApplicationBuilder.Services.AddControllers().AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			});

			webApplicationBuilder.Services.AddSwaggerServices();



			webApplicationBuilder.Services.AddDbContext<StoreContext>(option =>
			{
				option.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			});

			webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
			{
				var connection = webApplicationBuilder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			});

			webApplicationBuilder.Services.AddApplicationServices();

			webApplicationBuilder.Services.AddDbContext<ApplicationIdentityDbContext>(option =>
			{
				option.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
			});

			webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{

			}).AddEntityFrameworkStores<ApplicationIdentityDbContext>();



			webApplicationBuilder.Services.AddAuthServices(webApplicationBuilder.Configuration);

			webApplicationBuilder.Services.AddCors(option =>
			{
				option.AddPolicy("MyPolicy", policyOptions =>
				{
					policyOptions.AllowAnyHeader().AllowAnyMethod().WithOrigins(webApplicationBuilder.Configuration["FrontBaseUrl"]);
				});
			});


			#endregion

			var app = webApplicationBuilder.Build();

			#region apply all pending migrations [update-database] and data seeding
			using var scop = app.Services.CreateScope();

			var services = scop.ServiceProvider;

			var _dbContext = services.GetRequiredService<StoreContext>();// ask clr for creating object from dbcontext explicitly 
			var _IdentityDbContext = services.GetRequiredService<ApplicationIdentityDbContext>();// ask clr for creating object from dbcontext explicitly 
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			var logger = loggerFactory.CreateLogger<Program>();



			try
			{
				await _dbContext.Database.MigrateAsync();// update database 
				await StoreContextSeed.SeedAsync(_dbContext);


				await _IdentityDbContext.Database.MigrateAsync(); // update database 
				var _userManger = services.GetRequiredService<UserManager<ApplicationUser>>();
				await ApplicationIdentityContextSeed.SeedUserAsync(_userManger);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);

				logger.LogError(ex.StackTrace, "an error has been occured during apply migration");
			}  
			#endregion

			#region configure() method to configure kestrel middlewares like dot net 5 

			app.UseMiddleware<ExceptionMiddleware>();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddlewares();
			}

			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseCors("MyPolicy");

			app.MapControllers();

			#endregion

		

			app.Run();
		}
	}
}
