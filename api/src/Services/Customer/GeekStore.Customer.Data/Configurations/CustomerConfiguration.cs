using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Customer.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Domain.Customers.Customer>
    {
        public void Configure(EntityTypeBuilder<Domain.Customers.Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Addresses)
                   .WithOne(x => x.Customer)
                   .HasForeignKey(x => x.CustomerId);

            builder.Property(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.Surname)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.Birthday)
                   .IsRequired();

            builder.Property(x => x.Document)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.ProfileImage)
                   .IsRequired(false)
                   .HasMaxLength(300);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(512);
        }
    }
}
