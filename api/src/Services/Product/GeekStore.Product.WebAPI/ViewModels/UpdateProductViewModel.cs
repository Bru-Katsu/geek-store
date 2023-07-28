using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GeekStore.Product.WebAPI.ViewModels
{
    [DisplayName("UpdateProduct")]
    public class UpdateProductViewModel
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
