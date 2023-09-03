using GeekStore.Customer.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Customer.Data.Context
{
    public class CustomerDataContext : DbContext
    {
        public CustomerDataContext() { }
        public CustomerDataContext(DbContextOptions<CustomerDataContext> options) : base(options) { }

        public DbSet<Domain.Customers.Customer> Customers { get; set; }
        public DbSet<Domain.Customers.CustomerAddress> CustomerAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerAddressConfiguration());
        }
    }
}
