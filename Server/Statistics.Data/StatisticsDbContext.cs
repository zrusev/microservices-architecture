namespace Statistics.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Reflection;

    public class StatisticsDbContext : DbContext
    {
        public StatisticsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<SeenProduct> SeenProducts { get; set; }

        public DbSet<BoughtProduct> BoughtProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}