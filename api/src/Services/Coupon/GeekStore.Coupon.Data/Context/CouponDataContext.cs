using Microsoft.EntityFrameworkCore;

namespace GeekStore.Coupon.Data.Context
{
    public class CouponDataContext : DbContext
    {
        public CouponDataContext() { }
        public CouponDataContext(DbContextOptions<CouponDataContext> options) : base(options) { }

        public DbSet<Domain.Coupons.Coupon> Coupons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Coupons.Coupon>().HasData(new Domain.Coupons.Coupon("GEEK10", 10));
            modelBuilder.Entity<Domain.Coupons.Coupon>().HasData(new Domain.Coupons.Coupon("GEEK20", 20));
            modelBuilder.Entity<Domain.Coupons.Coupon>().HasData(new Domain.Coupons.Coupon("GEEK25", 25));
        }
    }
}
