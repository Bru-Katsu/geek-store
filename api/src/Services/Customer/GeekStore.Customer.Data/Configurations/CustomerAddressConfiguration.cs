using GeekStore.Customer.Domain.Customers;
using GeekStore.Customer.Domain.Customers.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Customer.Data.Configurations
{
    public class CustomerAddressConfiguration : IEntityTypeConfiguration<Domain.Customers.CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.Addresses)
                   .HasForeignKey(x => x.CustomerId);

            builder.Property(a => a.Street)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(a => a.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.State)
                   .IsRequired()
                   .HasMaxLength(2);

            builder.Property(a => a.Country)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(a => a.ZipCode)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.Type)
                   .IsRequired()
                   .HasDefaultValue(AddressType.Common);
        }
    }
}
