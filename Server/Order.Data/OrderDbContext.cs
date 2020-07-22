namespace Order.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using StoreApi.Data;
    using System.Reflection;

    public class OrderDbContext : MessageDbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}