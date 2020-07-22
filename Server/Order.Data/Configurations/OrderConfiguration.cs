namespace Order.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .Property(k => k.Id)
                .UseHiLo("OrderIdHiLoSequence");
        }
    }
}
