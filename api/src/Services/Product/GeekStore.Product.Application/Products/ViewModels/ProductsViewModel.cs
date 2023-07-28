using System.ComponentModel.DataAnnotations;

namespace GeekStore.Product.Application.Products.ViewModels
{
    public class ProductsViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}