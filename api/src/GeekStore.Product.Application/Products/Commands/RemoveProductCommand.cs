using FluentValidation;
using GeekStore.Core.Commands;

namespace GeekStore.Product.Application.Products.Commands
{
    public sealed class RemoveProductCommand : Command
    {
        public RemoveProductCommand(Guid productId)
        {
            Id = productId;
        }

        public Guid Id { get; }

        public override bool IsValid()
        {
            ValidationResult = new RemoveProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class RemoveProductCommandValidation : AbstractValidator<RemoveProductCommand>
        {
            public RemoveProductCommandValidation()
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id inválido!");
            }
        }
    }
}
