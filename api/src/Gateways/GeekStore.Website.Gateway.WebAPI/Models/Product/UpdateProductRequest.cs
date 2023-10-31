using System.ComponentModel.DataAnnotations;

namespace GeekStore.Website.Gateway.WebAPI.Models.Product
{
    public class UpdateProductRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageURL { get; set; }
    }
}
