using FluentValidation;
using GeekStore.Core.Helpers;
using GeekStore.Core.Messages;

namespace GeekStore.Product.Application.Products.Commands
{
    public sealed class UpdateProductCommand : Command
    {
        public UpdateProductCommand(Guid id, string name, decimal price, string description, string category, string imageURL)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            Category = category;
            ImageURL = imageURL;
        }

        public Guid Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public string Description { get; }
        public string Category { get; }
        public string ImageURL { get; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
        {
            public UpdateProductCommandValidation()
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id inválido!");

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
            }
        }
    }
}
