namespace Statistics.Web
{
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Notifications.Web.Messages;
    using Serilog;
    using Statistics.Data;
    using StoreApi.Web.Infrastructure;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;
        
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddCors()
                .AddDatabase<StatisticsDbContext>(
                    this.Configuration.GetConnectionString("DefaultConnection"))
                .AddTokenHandler(
                    this.Configuration.GetSection("AppSettings"))
                .AddConventionalServices()
                .AddMappingServices()
                .AddMessaging(
                    this.Configuration.GetSection("MassTransitCredentials"),
                    typeof(SeenProductConsumer))
                .AddControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .UseHealthChecksConfig()
                .UseSerilogRequestLogging()
                .UseInitializer(env)
                .UseDataSeed().GetAwaiter().GetResult();
    }
}