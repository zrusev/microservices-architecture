namespace Customer.Gateway.Web
{
    using Customer.Gateway.Services.Contracts;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Refit;
    using Serilog;
    using StoreApi.Web.Infrastructure;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var serviceEndpoints = this.Configuration
                .GetSection(nameof(ServiceEndpoints))
                .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
                .AddCors()
                .AddTokenHandler(
                    this.Configuration.GetSection("AppSettings"))
                .AddConventionalServices()
                .AddMappingServices()
                .AddControllers();

            services
                .AddRefitClient<IProductsService>()
                .WithConfiguration(serviceEndpoints.Customers);

            services
                .AddRefitClient<ITopBoughtProductsService>()
                .WithConfiguration(serviceEndpoints.Statistics);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
            => app
               .UseWebService(env)
               .UseSerilogRequestLogging();
    }
}
