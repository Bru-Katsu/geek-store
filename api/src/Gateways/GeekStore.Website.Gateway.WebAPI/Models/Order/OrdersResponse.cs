namespace GeekStore.Website.Gateway.WebAPI.Models.Order
{
    public class OrdersResponse
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int ItemsCount { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDiscount { get; set; }
    }
}
