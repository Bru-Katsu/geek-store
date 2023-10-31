namespace GeekStore.Order.WebAPI.Requests
{
    public class CreateOrderRequest
    {
        public IEnumerable<OrderItemRequest> Items { get; set; }
        public OrderAddressRequest Address { get; set; }
        public OrderCouponRequest? Coupon { get; set; }
    }

    public class OrderAddressRequest
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class OrderCouponRequest
    {
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    public class OrderItemRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
