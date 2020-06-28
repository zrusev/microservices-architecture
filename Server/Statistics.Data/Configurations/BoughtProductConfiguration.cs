namespace Statistics.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class BoughtProductConfiguration : IEntityTypeConfiguration<BoughtProduct>
    {
        public void Configure(EntityTypeBuilder<BoughtProduct> builder)
        {
            builder
                .HasKey(v => v.Id);

            builder
                .HasIndex(v => v.ProductId);

            builder
                .Property(v => v.UserId)
                .IsRequired();
        }
    }
}
