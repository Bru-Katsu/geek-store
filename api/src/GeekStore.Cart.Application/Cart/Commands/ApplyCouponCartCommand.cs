using FluentValidation;
using GeekStore.Core.Messages;

namespace GeekStore.Cart.Application.Cart.Commands
{
    public class ApplyCouponCartCommand : Command
    {
        public ApplyCouponCartCommand(Guid userId, Guid couponId)
        {
            UserId = userId;
            CouponId = couponId;
        }

        public Guid UserId { get; }
        public Guid CouponId { get; }

        public override bool IsValid()
        {
            ValidationResult = new ApplyCouponCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class ApplyCouponCommandValidation : AbstractValidator<ApplyCouponCartCommand>
        {
            public ApplyCouponCommandValidation()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty()
                    .WithMessage("Id de usuário inválido!");

                RuleFor(x => x.CouponId)
                    .NotEmpty()
                    .WithMessage("Id de cupom inválido!");
            }
        }
    }
}
