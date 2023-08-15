using GeekStore.Order.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Order.Data.Context
{
    public class OrderDataContext : DbContext
    {
        public OrderDataContext() { }
        public OrderDataContext(DbContextOptions<OrderDataContext> options) : base(options) { }

        public DbSet<Domain.Orders.Order> Orders { get; set; }
        public DbSet<Domain.Orders.OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>()
                                .HavePrecision(18, 6);
        }
    }
}
