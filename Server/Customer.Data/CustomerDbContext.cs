namespace Customer.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using StoreApi.Data;
    using System.Reflection;

    public class CustomerDbContext : MessageDbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}
