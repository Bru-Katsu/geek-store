namespace GeekStore.Website.Gateway.WebAPI.Models.Coupon
{
    public class CouponResponse
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
