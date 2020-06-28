namespace Statistics.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class SeenProductConfiguration : IEntityTypeConfiguration<SeenProduct>
    {
        public void Configure(EntityTypeBuilder<SeenProduct> builder)
        {
            builder
                .HasKey(v => v.Id);

            builder
                .HasIndex(v => v.ProductId);
        }
    }
}
