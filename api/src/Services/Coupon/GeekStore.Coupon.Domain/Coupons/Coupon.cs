using FluentValidation;
using GeekStore.Core.Models;

namespace GeekStore.Coupon.Domain.Coupons
{
    public class Coupon : Entity
    {
        protected Coupon() { }
        public Coupon(string couponCode, decimal discountAmount)
        {
            Id = Guid.NewGuid();
            CouponCode = couponCode?.ToUpper().Trim();
            DiscountAmount = discountAmount;
        }

        public string CouponCode { get; private set; }
        public void ChangeCouponCode(string couponCode)
        {
            CouponCode = couponCode?.ToUpper().Trim();
        }
        
        public decimal DiscountAmount { get; private set; }
        public void ChangeDiscountAmount(decimal discountAmount)
        {
            DiscountAmount = discountAmount;
        }

        private class CouponEntityValidation : AbstractValidator<Coupon>
        {
            public CouponEntityValidation()
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id do cupom inválido!");

                RuleFor(x => x.CouponCode)
                    .NotEmpty()
                    .WithMessage("Id do cupom não pode estar em branco!");

                RuleFor(x => x.DiscountAmount)
                    .GreaterThan(0)
                    .WithMessage("Percentagem de desconto inválido!");
            }
        }
    }
}
