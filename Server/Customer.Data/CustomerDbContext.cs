namespace Customer.Data
{
    using Models.Customers;
    using Models.Products;
    using Microsoft.EntityFrameworkCore;

    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
