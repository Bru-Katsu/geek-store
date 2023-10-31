using FluentValidation;
using GeekStore.Core.Messages;

namespace GeekStore.Customer.Application.Addresses.Commands
{
    public class RemoveAddressCommand : Command
    {
        public RemoveAddressCommand(Guid customerId, Guid addressId)
        {
            CustomerId = customerId;
            AddressId = addressId;
        }

        public Guid CustomerId { get; }
        public Guid AddressId { get; }

        public override bool IsValid()
        {
            ValidationResult = new RemoveAddressCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class RemoveAddressCommandValidation : AbstractValidator<RemoveAddressCommand>
        {
            public RemoveAddressCommandValidation()
            {
                RuleFor(x => x.CustomerId)
                    .NotEmpty()
                    .WithMessage("Id do cliente inválido!");

                RuleFor(x => x.CustomerId)
                    .NotEmpty()
                    .WithMessage("Id do endereço inválido!");
            }
        }
    }
}
