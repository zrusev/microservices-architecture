namespace Customer.Web
{
    using Customer.Data;
    using Customer.Web.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using StoreApi.Web.Infrastructure;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;       

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        { 
            services
                .AddCors()
                .AddDatabase<CustomerDbContext>(this.Configuration)
                .AddTokenHandler(this.Configuration.GetSection("AppSettings"))
                .AddConventionalServices()
                .AddMappingServices()
                .AddMessaging()
                .AddControllers();
        } 
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
               .UseWebService(env)
               .UseSerilogRequestLogging()
               .UseInitializer(env);
    }
}