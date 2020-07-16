namespace Notifications.Web
{
    using Hub;
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Notifications.Web.Messages;
    using Serilog;
    using StoreApi.Web.Infrastructure;

    using static WebConstants;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddCors()
                .AddTokenHandler(
                    this.Configuration.GetSection("AppSettings"),
                    JwtConfiguration.BearerEvents) 
                .AddMappingServices()
                .AddMessaging(
                    this.Configuration.GetSection("MassTransitCredentials"), 
                    typeof(CustomerCreatedConsumer))
                .AddSignalR();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            
            app
                .UseRouting()
                .UseCors(options => options
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials())
                .UseAuthentication()
                .UseAuthorization()
                .UseSerilogRequestLogging()
                .UseEndpoints(endpoints => endpoints
                .MapHub<NotificationsHub>(NotificationEndpoint));
        }
    }
}
