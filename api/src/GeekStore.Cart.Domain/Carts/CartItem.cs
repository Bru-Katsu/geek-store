using FluentValidation;
using GeekStore.Core.Models;

namespace GeekStore.Cart.Domain.Carts
{
    public class CartItem : Entity
    {
        public CartItem(Guid productId, string productName, int quantity, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }

        public Guid ProductId { get; private set; }

        public string ProductName { get; private set; }
        public void SetName(string name)
        {
            ProductName = name;
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
                    .WithMessage("Id inválido!");

                RuleFor(x => x.Price)
                    .GreaterThan(0)
                    .WithMessage("Preço não pode ser menor ou igual a zero!");
            }
        }
    }
}
