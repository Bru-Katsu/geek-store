using System.ComponentModel.DataAnnotations;

namespace GeekStore.Cart.Application.Cart.ViewModels
{
    public class CartItemViewModel
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
