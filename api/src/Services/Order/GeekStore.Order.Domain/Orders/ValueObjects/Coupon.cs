using FluentValidation;
using FluentValidation.Results;
using GeekStore.Core.Models;

namespace GeekStore.Order.Domain.Orders.ValueObjects
{
    public class Coupon : ValueObject
    {
        public Coupon(string couponCode, decimal? discountAmount)
        {
            CouponCode = couponCode;
            DiscountAmount = discountAmount;
        }

        public string CouponCode { get; }
        public decimal? DiscountAmount { get; }

        public override ValidationResult Validate()
        {
            ValidationResult = new CouponEntityValidator().Validate(this);
            return ValidationResult;
        }

        private class CouponEntityValidator : AbstractValidator<Coupon>
        {
            public CouponEntityValidator()
            {
                RuleFor(x => x.CouponCode)
                    .NotEmpty()
                    .When(x => x.CouponCode != null)
                    .WithMessage("Código do cupom não pode ficar em branco!")
                    .MaximumLength(10)
                    .WithMessage("O código do cupom deve conter no máximo 10 caracteres!");

                RuleFor(x => x.DiscountAmount)
                    .GreaterThan(0)
                    .WithMessage("Percentual de desconto inválido!");
            }
        }
    }
}
