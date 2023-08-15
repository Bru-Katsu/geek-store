using GeekStore.Order.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Order.Data.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(orderItem => orderItem.Id);

            builder.HasOne(orderItem => orderItem.Order)
                   .WithMany(order => order.OrderItems)
                   .HasForeignKey(orderItem => orderItem.Id);

            builder.Property(orderItem => orderItem.ProductId)
                   .IsRequired();

            builder.Property(orderItem => orderItem.ProductName)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(orderItem => orderItem.ProductImage)
                   .HasMaxLength(512)
                   .IsRequired();

            builder.Property(orderItem => orderItem.Quantity)
                   .IsRequired();

            builder.Property(orderItem => orderItem.Price)
                   .IsRequired();
        }
    }
}
