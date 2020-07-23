namespace Customer.Web
{
    using Customer.Data;
    using Messages;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using StoreApi.Web.Infrastructure;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        { 
            services
                .AddCors()
                .AddDatabase<CustomerDbContext>(
                    this.Configuration.GetConnectionString("DefaultConnection"))
                .AddTokenHandler(
                    this.Configuration.GetSection("AppSettings"))
                .AddConventionalServices()
                .AddMappingServices()
                .AddMessaging(
                    this.Configuration.GetSection("MassTransitCredentials"),
                    typeof(OrderConsumer))
                .AddMessagingWorker(
                    this.Configuration.GetConnectionString("DefaultConnection"))
                .AddHealth(
                    this.Configuration.GetSection("MassTransitCredentials"),
                    this.Configuration.GetConnectionString("DefaultConnection"))
                .AddControllers();
        } 
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
               .UseWebService(env)
               .UseHealthChecksConfig()
               .UseSerilogRequestLogging()
               .UseInitializer(env);
    }
}