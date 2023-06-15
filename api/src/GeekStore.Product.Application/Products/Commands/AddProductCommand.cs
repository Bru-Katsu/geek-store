using FluentValidation;
using GeekStore.Core.Commands;

namespace GeekStore.Product.Application.Products.Commands
{
    public sealed class AddProductCommand : Command
    {
        public AddProductCommand(string name, decimal price, string description, string category, string imageURL)
        {
            Name = name;
            Price = price;
            Description = description;
            Category = category;
            ImageURL = imageURL;
        }

        public string Name { get; }
        public decimal Price { get; }
        public string Description { get; }
        public string Category { get; }
        public string ImageURL { get; }

        public override bool IsValid()
        {
            ValidationResult = new AddProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class AddProductCommandValidation : AbstractValidator<AddProductCommand>
        {
            public AddProductCommandValidation()
            {
                RuleFor(x =>  x.Name)
                    .NotEmpty()
                    .WithMessage("O nome do produto está vazio!")
                    .MaximumLength(150)
                    .WithMessage("O nome deve conter no máximo 150 caracteres!");

                RuleFor(x => x.Price)
                    .GreaterThan(0)
                    .WithMessage("O valor do produto não pode ser menor ou igual a zero!");

                RuleFor(x => x.Description)
                    .MaximumLength(500)
                    .WithMessage("A descrição deve conter no máximo 500 caracteres!");

                RuleFor(x => x.Category)
                    .MaximumLength(50)
                    .WithMessage("A categoria deve conter no máximo 50 caracteres!");

                RuleFor(x => x.ImageURL)
                    .MaximumLength(300)
                    .WithMessage("O link da imagem deve conter no máximo 300 caracteres!");
            }
        }
    }
}
