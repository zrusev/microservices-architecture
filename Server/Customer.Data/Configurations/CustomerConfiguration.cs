namespace Customer.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    using static DataConstants.Common;
    using static DataConstants.Customers;

    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .Property(n => n.Email)
                .IsRequired()
                .HasMaxLength(MaxEmailLength);

            builder
                .Property(n => n.FirstName)
                .IsRequired()
                .HasMaxLength(MaxNameLength);
            
            builder
                .Property(n => n.LastName)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder
                .Property(n => n.Address1)
                .HasMaxLength(MaxAddressLength);

            builder
                .Property(n => n.Address2)
                .HasMaxLength(MaxAddressLength);

            builder
                .Property(n => n.PhoneNumber)
                .IsRequired()
                .HasMaxLength(MaxPhoneNumberLength);

            builder
                .Property(d => d.UserId)
                .IsRequired();
        }
    }
}
