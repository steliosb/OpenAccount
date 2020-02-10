using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Service.OpenAccount.Customers.Core;
using Service.OpenAccount.Customers.Core.Abstractions;
using Service.OpenAccount.Customers.Data;
using Service.OpenAccount.Customers.Data.Abstractions;
using Service.OpenAccount.Customers.Integration;
using Service.OpenAccount.Customers.Integration.Abstractions;
using System;
using System.IO;
using Serilog;

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

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.OpenAccount.Customers API", Version = "v1" });
				var controllerXmlFile = $"Service.OpenAccount.Customers.WebApi.xml";
				var controllerXmlPath = Path.Combine(AppContext.BaseDirectory, controllerXmlFile);
				c.IncludeXmlComments(controllerXmlPath);

			});
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
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service.OpenAccount.Customers , API v1");
			});

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
