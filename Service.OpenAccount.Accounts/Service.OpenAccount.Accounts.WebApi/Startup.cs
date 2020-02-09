using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.OpenApi.Models;
using Service.OpenAccount.Accounts.Core;
using Service.OpenAccount.Accounts.Core.Abstractions;
using Service.OpenAccount.Accounts.Data;
using Service.OpenAccount.Accounts.Data.Abstrsactions;
using Service.OpenAccount.Accounts.Integration;
using Service.OpenAccount.Accounts.Integration.Abstractions;

namespace Service.OpenAccount.Accounts.WebApi
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
			services.AddTransient<IAccountManager, AccountManager>();
			services.AddTransient<IAccountRepository, AccountRepository>();
			services.AddTransient<ITransactionServiceClient, TransactionServiceClient>();
			services.AddSingleton<ITransactionServiceClientConfig>(new TransactionServiceClientConfig()
			{
				Endpoint = Configuration["TransactionClient:EndPoint"]
			});
			services.AddTransient<ICustomerServiceClient, CustomerServiceClient>();
			services.AddSingleton<ICustomerServiceClientConfig>(new CustomerServiceClientConfig()
			{
				Endpoint = Configuration["CustomerClient:EndPoint"]
			});

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.OpenAccount.Accounts API", Version = "v1" });
				var controllerXmlFile = $"Service.OpenAccount.Accounts.WebApi.xml";
				var controllerXmlPath = Path.Combine(AppContext.BaseDirectory, controllerXmlFile);
				c.IncludeXmlComments(controllerXmlPath);

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

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service.OpenAccount.Accounts , API v1");
			});

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
