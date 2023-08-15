namespace GeekStore.Order.Application.Orders.DTOs
{
    public class OrderItemDTO
    {
        public OrderItemDTO(Guid productId, string productName, string productImage, int quantity, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            ProductImage = productImage;
            Quantity = quantity;
            Price = price;
        }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
