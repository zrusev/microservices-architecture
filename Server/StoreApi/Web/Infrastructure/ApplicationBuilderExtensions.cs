namespace StoreApi.Web.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseWebService(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            return app
                   .UseHttpsRedirection()
                   .UseRouting()
                   .UseCors(x => x
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader())
                   .UseAuthentication()
                   .UseAuthorization()
                   .UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public static IApplicationBuilder UseInitializer(this IApplicationBuilder app, IWebHostEnvironment env)
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