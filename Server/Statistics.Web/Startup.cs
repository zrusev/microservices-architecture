namespace Statistics.Web
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Statistics.Data;
    using Statistics.Web.Infrastructure;
    using StoreApi.Web.Infrastructure;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;
        
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                  .AddCors()
                  .AddDatabase<StatisticsDbContext>(this.Configuration)
                  .AddTokenHandler(this.Configuration.GetSection("AppSettings"))
                  .AddConventionalServices()
                  .AddAutoMapper(this.GetType())
                  .AddControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
               .UseWebService(env)
               .UseSerilogRequestLogging()
               .UseInitializer(env)
               .UseDataSeed().GetAwaiter().GetResult();
    }
}