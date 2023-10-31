namespace GeekStore.Website.Gateway.WebAPI.Models.Cart
{
    public class CartResponse
    {
        public Guid UserId { get; set; }
        public Guid? CouponId { get; set; }
        public IEnumerable<CartItemResponse> Items { get; set; }
    }

    public class CartItemResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
