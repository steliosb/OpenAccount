using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.OpenAccount.Customers.Core;
using Service.OpenAccount.Customers.Core.Abstractions;
using Service.OpenAccount.Customers.Data;
using Service.OpenAccount.Customers.Data.Abstractions;
using Service.OpenAccount.Customers.Integration;
using Service.OpenAccount.Customers.Integration.Abstractions;

namespace Service.OpenAccount.Customers.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<ICustomerManager, CustomerManager>();
			services.AddTransient<ICustomerRepository, CustomerRepository>();
			services.AddTransient<IAccountServiceClient, AccountServiceClient>();
			services.AddSingleton<IAccountServiceClientConfig>(new AccountServiceClientConfig()
			{
				EndPoint = Configuration["AccountClient:EndPoint"]
			});
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
