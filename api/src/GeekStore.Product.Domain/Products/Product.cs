using FluentValidation;
using GeekStore.Core.Helpers;
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

        public override bool IsValid()
        {            
            ValidationResult = new ProductEntityValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class ProductEntityValidation : AbstractValidator<Product>
        {
            public ProductEntityValidation()
            {
                RuleFor(x => x.Name)
                     .NotEmpty()
                     .WithMessage("Nome do produto não pode ficar em branco!")
                     .MaximumLength(150)
                     .WithMessage("O nome do produto deve conter no máximo 150 caracteres!");

                RuleFor(x => x.Price)
                    .GreaterThan(0)
                    .WithMessage("O Valor do produto não pode ficar zerado ou negativo!");

                RuleFor(x => x.Description)
                    .MaximumLength(500)
                    .WithMessage("A descrição do produto deve conter no máximo 500 caracteres!");

                RuleFor(x => x.Category)
                    .NotEmpty()
                    .WithMessage("A categoria do produto não pode ficar em branco!")
                    .MaximumLength(50)
                    .WithMessage("A categoria do produto deve conter no máximo 50 caracteres!");

                RuleFor(x => x.ImageURL)
                    .NotEmpty()
                    .WithMessage("A URL da imagem do produto não pode ficar em branco!")
                    .MaximumLength(300)
                    .WithMessage("A URL da imagem do produto deve conter no máximo 300 caracteres!")
                    .Must(UrlHelper.IsValidImageUrl)
                    .WithMessage("A URL da imagem do produto não é válida. Certifique-se de fornecer uma URL válida.");

                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("O ID do produto não pode ficar em branco!");
            }
        }
    }
}
