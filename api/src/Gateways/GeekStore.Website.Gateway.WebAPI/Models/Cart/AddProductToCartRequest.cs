using System.ComponentModel.DataAnnotations;

namespace GeekStore.Website.Gateway.WebAPI.Models.Cart
{
    public class AddProductToCartRequest
    {
        [Required]
        public Guid ProductId { get; set; }
        
        [Required]
        public int Quantity { get; set; }
    }
}
