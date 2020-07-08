namespace Customer.Data.Configurations
{
    using Customer.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static DataConstants.Common;
    using static DataConstants.Products;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .HasIndex(n => n.Name);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(MaxModelLength);

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasColumnType("text");

            builder
                .Property(c => c.Url)
                .IsRequired()
                .HasMaxLength(MaxUrlLength);

            builder
                .Property(c => c.Image_url)
                .IsRequired()
                .HasMaxLength(MaxUrlLength);

            builder
                .Property(c => c.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder
                .HasOne(c => c.Manufacturer)
                .WithMany(m => m.Products)
                .HasForeignKey(c => c.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
