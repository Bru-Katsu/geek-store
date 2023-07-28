using FluentValidation;
using GeekStore.Core.Models;

namespace GeekStore.Cart.Domain.Carts
{
    public class CartItem : Entity
    {
        public CartItem(Guid productId, string productName, int quantity, decimal price)
        {
            Id = productId;
            Name = productName;
            Quantity = quantity;
            Price = price;
        }

        public string Name { get; private set; }
        public void SetName(string name)
        {
            Name = name;
        }

        public int Quantity { get; private set; }
        public void ChangeQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public decimal Price { get; private set; }
        public void ChangePrice(decimal price)
        {
            Price = price;
        }

        public override bool IsValid()
        {
            ValidationResult = new CartItemEntityValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class CartItemEntityValidation : AbstractValidator<CartItem>
        {
            public CartItemEntityValidation()
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id do produto inválido!");

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Nome do produto não pode ficar vazio!");

                RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantidade não pode ser menor ou igual a zero!");

                RuleFor(x => x.Price)
                    .GreaterThan(0)
                    .WithMessage("Preço não pode ser menor ou igual a zero!");
            }
        }
    }
}
