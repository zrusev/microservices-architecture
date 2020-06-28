namespace Statistics.Web.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Statistics.Data;
    using Statistics.Data.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public static class DataCollectionExtensions
    {
        public static async Task UseDataSeed(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;

                var db = serviceProvider.GetRequiredService<StatisticsDbContext>();

                if (db.SeenProducts.Any())
                {
                    return;
                }

                db.SeenProducts.Add(new SeenProduct
                {
                    TotalVisits = 0
                });

                await db.SaveChangesAsync();
            }
        }
    }
}