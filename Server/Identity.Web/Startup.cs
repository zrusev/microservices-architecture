namespace Identity.Web
{
    using AutoMapper;
    using Identity.Data;
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using StoreApi.Infrastructure;
    using System;

    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors()
                .AddDatabase<ApplicationDbContext>(_configuration)
                .AddUserStorage()
                .AddTokenHandler(_configuration.GetSection("AppSettings"))
                .AddConventionalServices()
                .AddAutoMapper(this.GetType())
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app
               .UseWebService(env)
               .UseSerilogRequestLogging()
               .Initialize()
               .UseDataSeed(services, _configuration).Wait();
        }
    }
}
