using FluentValidation;
using GeekStore.Core.Messages;
using GeekStore.Core.Results;
using GeekStore.Order.Application.Orders.DTOs;

namespace GeekStore.Order.Application.Orders.Commands
{
    public class CreateOrderCommand : Command<Result<Guid>>
    {
        public CreateOrderCommand(Guid userId, AddressDTO address, CouponDTO coupon, IEnumerable<OrderItemDTO> items)
        {
            UserId = userId;
            Address = address;
            Coupon = coupon;
            Items = items;
        }

        public Guid UserId { get; }

        public AddressDTO Address { get; }
        public CouponDTO Coupon { get; }

        public IEnumerable<OrderItemDTO> Items { get; }

        public override bool IsValid()
        {
            ValidationResult = new CreateOrderCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        private class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
        {
            public CreateOrderCommandValidator()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty()
                    .WithMessage("Id de usuário inválido!");

                RuleFor(x => x.Address)
                    .ChildRules(child =>
                    {
                        child.RuleFor(x => x.Street)
                             .NotEmpty()
                             .WithMessage("Nome da rua não pode ficar em branco!")
                             .MaximumLength(150)
                             .WithMessage("A rua deve conter no máximo 150 caracteres!");

                        child.RuleFor(x => x.City)
                             .NotEmpty()
                             .WithMessage("Nome da cidade não pode ficar em branco!")
                             .MaximumLength(100)
                             .WithMessage("A cidade deve conter no máximo 100 caracteres!");

                        child.RuleFor(x => x.State)
                             .NotEmpty()
                             .WithMessage("O estado não pode ficar em branco!")
                             .MaximumLength(2)
                             .WithMessage("O estado deve conter no máximo 2 caracteres!");

                        child.RuleFor(x => x.Country)
                             .NotEmpty()
                             .WithMessage("O País não pode ficar em branco!")
                             .MaximumLength(50)
                             .WithMessage("O País deve conter no máximo 50 caracteres!");

                        child.RuleFor(x => x.ZipCode)
                             .NotEmpty()
                             .WithMessage("Código postal não pode ficar em branco!")
                             .MaximumLength(20)
                             .WithMessage("O código postal deve conter no máximo 100 caracteres!");
                    });

                RuleFor(x => x.Coupon)
                    .ChildRules(child =>
                    {
                        child.RuleFor(x => x.CouponCode)
                             .NotEmpty()
                             .When(x => x.CouponCode != null)
                             .WithMessage("Código do cupom não pode ficar em branco!")
                             .MaximumLength(10)
                             .WithMessage("O código do cupom deve conter no máximo 10 caracteres!");

                        child.RuleFor(x => x.DiscountAmount)
                             .GreaterThan(0)
                             .WithMessage("Percentual de desconto inválido!");
                    })
                    .When(x => x.Coupon != null);

                RuleForEach(x => x.Items)
                    .ChildRules(child =>
                    {
                        child.RuleFor(x => x.ProductId)
                             .NotEmpty()
                             .WithMessage("Id de produto inválido!");

                        child.RuleFor(x => x.ProductName)
                             .NotEmpty()
                             .WithMessage("O nome do produto não pode estar em branco!")
                             .MaximumLength(255)
                             .WithMessage("O nome do produto deve conter no máximo 255 caracteres!");

                        child.RuleFor(x => x.ProductImage)
                             .NotEmpty()
                             .WithMessage("O link de imagem do produto não pode estar em branco!")
                             .MaximumLength(512)
                             .WithMessage("O link da imagem do produto deve conter no máximo 512 caracteres!");

                        child.RuleFor(x => x.Quantity)
                             .GreaterThan(0)
                             .WithMessage("A quantidade não pode ser menor ou igual a zero!");

                        child.RuleFor(x => x.Price)
                             .GreaterThanOrEqualTo(0)
                             .WithMessage("O preço do produto não pode ser menor que zero!");
                    });
            }
        }
    }
}
