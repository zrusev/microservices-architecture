namespace StoreApi.Web.Infrastructure
{
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseWebService(this IApplicationBuilder app, 
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            return app
                   .UseHttpsRedirection()
                   .UseRouting()
                   .UseCors(options => options
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader())
                   .UseAuthentication()
                   .UseAuthorization()
                   .UseEndpoints(endpoints => endpoints
                        .MapControllers());
        }

        public static IApplicationBuilder UseHealthChecksConfig(this IApplicationBuilder app)
            => app
                .UseEndpoints(endpoints =>
                {
                    endpoints.UseHealthExtension();

                    endpoints.MapControllers();
                });

        public static IEndpointRouteBuilder UseHealthExtension(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            return endpoints;
        }

        public static IApplicationBuilder UseInitializer(this IApplicationBuilder app, 
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            { 
                var serviceProvider = serviceScope.ServiceProvider;

                var db = serviceProvider.GetRequiredService<DbContext>();

                db.Database.Migrate();
            }

            return app;
        }
    }
}