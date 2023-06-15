using GeekStore.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace GeekStore.Product.Domain.Products
{
    public class Product : Entity
    {
        public Product(string name, decimal price, string description, string category, string imageURL)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Description = description;
            Category = category;
            ImageURL = imageURL;
        }

        [Required]
        [StringLength(150)]
        public string Name { get; private set; }
        public void SetName(string name)
        {
            Name = name;
        }

        [Required]
        public decimal Price { get; private set; }
        public void ChangePrice(decimal price)
        {
            Price = price;
        }

        [StringLength(500)]
        public string Description { get; private set; }
        public void SetDescription(string description)
        {
            Description = description;
        }


        [StringLength(50)]
        public string Category { get; private set; }
        public void ChangeCategory(string category)
        {
            Category = category;
        }

        [StringLength(300)]
        public string ImageURL { get; private set; }

        public void ChangeImageUrl(string url)
        {
            ImageURL = url;
        }
    }
}
