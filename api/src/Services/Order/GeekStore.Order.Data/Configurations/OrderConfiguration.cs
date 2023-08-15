using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Order.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Domain.Orders.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Orders.Order> builder)
        {
            builder.HasKey(order => order.Id);

            builder.HasMany(order => order.OrderItems)
                   .WithOne(orderItem => orderItem.Order)
                   .HasForeignKey(orderItem => orderItem.OrderId);

            builder.Property(order => order.UserId)
                   .IsRequired();

            builder.Property(order => order.CreationDate)
                   .IsRequired();

            builder.Property(order => order.Status)
                   .IsRequired();

            builder.OwnsOne(order => order.Address, address =>
            {
                address.Property(a => a.Street)
                       .HasMaxLength(150);

                address.Property(a => a.City)
                       .HasMaxLength(100);

                address.Property(a => a.State)
                       .HasMaxLength(2);

                address.Property(a => a.Country)
                       .HasMaxLength(50);

                address.Property(a => a.ZipCode)
                       .HasMaxLength(20);
            });

            builder.OwnsOne(order => order.Coupon, coupon =>
            {
                coupon.Property(c => c.CouponCode)
                      .HasMaxLength(10);

                coupon.Property(c => c.DiscountAmount);
            });
        }
    }
}
