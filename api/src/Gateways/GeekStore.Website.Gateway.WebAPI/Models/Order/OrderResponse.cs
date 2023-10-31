namespace GeekStore.Website.Gateway.WebAPI.Models.Order
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public AddressOrderResponse Address { get; set; }
        public CouponOrderResponse? Coupon { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDiscount { get; set; }

        public IEnumerable<OrderItemResponse> Items { get; set; }
    }

    public class AddressOrderResponse
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class CouponOrderResponse
    {
        public string CouponCode { get; set; }
        public decimal? DiscountAmount { get; set; }
    }

    public class OrderItemResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
