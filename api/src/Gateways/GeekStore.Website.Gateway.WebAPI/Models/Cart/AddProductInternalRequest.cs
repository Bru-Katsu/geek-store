using System.ComponentModel.DataAnnotations;

namespace GeekStore.Website.Gateway.WebAPI.Models.Cart
{
    public class AddProductInternalRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
