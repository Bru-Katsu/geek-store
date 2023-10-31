namespace GeekStore.Website.Gateway.WebAPI.Models.Product
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageURL { get; set; }
    }
}
