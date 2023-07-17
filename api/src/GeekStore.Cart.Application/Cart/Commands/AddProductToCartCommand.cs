using FluentValidation;
using GeekStore.Core.Messages;

namespace GeekStore.Cart.Application.Cart.Commands
{
    public class AddProductToCartCommand : Command
    {
        public AddProductToCartCommand(Guid userId, Guid productId, string productName, int productQuantity, decimal productPrice)
        {
            UserId = userId;
            ProductId = productId;
            ProductName = productName;
            ProductQuantity = productQuantity;
            ProductPrice = productPrice;
        }

        public Guid UserId { get; }

        public Guid ProductId { get; }
        public string ProductName { get; }
        public int ProductQuantity { get; }
        public decimal ProductPrice { get; }

        public override bool IsValid()
        {
            ValidationResult = new AddProductToCartCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class AddProductToCartCommandValidation : AbstractValidator<AddProductToCartCommand>
        {
            public AddProductToCartCommandValidation()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty()
                    .WithMessage("Id de usuário inválido!");

                RuleFor(x => x.ProductId)
                    .NotEmpty()
                    .WithMessage("Id do produto inválido!");

                RuleFor(x => x.ProductName)
                    .NotEmpty()
                    .WithMessage("Nome do produto inválido!");

                RuleFor(x => x.ProductQuantity)
                    .GreaterThan(0)
                    .WithMessage("A quantidade não pode ser menor ou igual a zero!");

                RuleFor(x => x.ProductPrice)
                    .GreaterThan(0)
                    .WithMessage("O valor não pode ser menor ou igual a zero!");
            }
        }
    }
}
