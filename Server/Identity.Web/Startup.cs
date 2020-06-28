namespace Identity.Web
{
    using AutoMapper;
    using Identity.Data;
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using StoreApi.Web.Infrastructure;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;
     
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddCors()
                .AddDatabase<ApplicationDbContext>(this.Configuration)
                .AddUserStorage()
                .AddTokenHandler(this.Configuration.GetSection("AppSettings"))
                .AddConventionalServices()
                .AddAutoMapper(this.GetType())
                .AddControllers();
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
            => app
               .UseWebService(env)
               .UseSerilogRequestLogging()
               .UseInitializer(env)
               .UseDataSeed(services, this.Configuration).GetAwaiter().GetResult();
    }
}