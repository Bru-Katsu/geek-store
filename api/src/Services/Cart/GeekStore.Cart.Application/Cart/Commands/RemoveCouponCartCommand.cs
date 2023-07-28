using FluentValidation;
using GeekStore.Core.Messages;

namespace GeekStore.Cart.Application.Cart.Commands
{
    public class RemoveCouponCartCommand : Command
    {
        public RemoveCouponCartCommand(Guid userId)
        {
            UserId = userId;    
        }

        public Guid UserId { get; }

        public override bool IsValid()
        {
            ValidationResult = new RemoveCouponCartCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class RemoveCouponCartCommandValidation : AbstractValidator<RemoveCouponCartCommand>
        {
            public RemoveCouponCartCommandValidation()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty()
                    .WithMessage("Id de usuário inválido!");
            }
        }
    }
}
