namespace GeekStore.Coupon.Application.Coupons.ViewModels
{
    public class CouponViewModel
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
