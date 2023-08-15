using FluentValidation;
using GeekStore.Core.Models;

namespace GeekStore.Order.Domain.Orders
{
    public class OrderItem : Entity
    {
        protected OrderItem() { }
        public OrderItem(Guid productId, string productName, string productImage, int quantity, decimal price)
        {
            Id = Guid.NewGuid();

            ProductId = productId;
            ProductName = productName;
            ProductImage = productImage;
            Quantity = quantity;
            Price = price;
        }

        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }

        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductImage { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public decimal CalculateValue()
        {
            return Quantity * Price;
        }

        public override bool IsValid()
        {
            ValidationResult = new OrderItemEntityValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class OrderItemEntityValidation : AbstractValidator<OrderItem>
        {
            public OrderItemEntityValidation()
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id inválido!");

                RuleFor(x => x.ProductId)
                    .NotEmpty()
                    .WithMessage("Id de produto inválido!");

                RuleFor(x => x.ProductName)
                    .NotEmpty()
                    .WithMessage("O nome do produto não pode estar em branco!")
                    .MaximumLength(255)
                    .WithMessage("O nome do produto deve conter no máximo 255 caracteres!");

                RuleFor(x => x.ProductImage)
                    .NotEmpty()
                    .WithMessage("O link de imagem do produto não pode estar em branco!")
                    .MaximumLength(512)
                    .WithMessage("O link da imagem do produto deve conter no máximo 512 caracteres!");

                RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .WithMessage("A quantidade não pode ser menor ou igual a zero!");

                RuleFor(x => x.Price)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("O preço do produto não pode ser menor que zero!");
            }
        }
    }
}
