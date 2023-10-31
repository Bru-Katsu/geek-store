namespace GeekStore.Website.Gateway.WebAPI.Models.Order
{
    public class CreateOrderInternalRequest
    {
        public IEnumerable<ItemOrderInternalRequest> Items { get; set; }
        public AddressOrderInternalRequest Address { get; set; }
        public CouponOrderInternalRequest Coupon { get; set; }
    }

    public class AddressOrderInternalRequest
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class CouponOrderInternalRequest
    {
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    public class ItemOrderInternalRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
