using FluentValidation;
using GeekStore.Core.Messages;

namespace GeekStore.Cart.Application.Cart.Commands
{
    public class RemoveProductCartCommand : Command
    {
        public RemoveProductCartCommand(Guid userId, Guid productId)
        {
            UserId = userId;
            ProductId = productId;
        }

        public Guid UserId { get; }
        public Guid ProductId { get; }

        public override bool IsValid()
        {
            ValidationResult = new RemoveProductFromCartCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class RemoveProductFromCartCommandValidation : AbstractValidator<RemoveProductCartCommand>
        {
            public RemoveProductFromCartCommandValidation()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty()
                    .WithMessage("Id de usuário inválido!");

                RuleFor(x => x.ProductId)
                    .NotEmpty()
                    .WithMessage("Id do produto inválido!");
            }
        }
    }
}
