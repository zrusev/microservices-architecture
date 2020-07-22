namespace Order.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder
                .HasKey(i => new { i.OrderId, i.ItemId });

            builder
                .HasOne(p => p.Order)
                .WithMany(o => o.Products)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(o => o.OrderId)
                .IsRequired();

            builder
                .Property(p => p.ProductId)
                .IsRequired();
        }
    }
}
