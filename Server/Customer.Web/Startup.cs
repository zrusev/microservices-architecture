namespace Customer.Web
{
    using AutoMapper;
    using Customer.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
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
                .AddDbContext<CustomerDbContext>(options =>
                    options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")))
                .AddTokenHandler(_configuration.GetSection("AppSettings"))
                .AddConventionalServices()
                .AddAutoMapper(this.GetType())
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app
               .UseHttpsRedirection()
               .UseRouting()
               .UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
               )
               .UseSerilogRequestLogging()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
