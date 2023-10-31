using FluentValidation;
using GeekStore.Core.Messages;
using GeekStore.Core.Results;

namespace GeekStore.Customer.Application.Addresses.Commands
{
    public class AddAddressCommand : Command<Result<Guid>>
    {
        public AddAddressCommand(Guid customerId, string street, string city, string state, string country, string zipCode, int type)
        {
            CustomerId = customerId;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            Type = type;
        }

        public Guid CustomerId { get; }
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }
        public int Type { get; }

        public override bool IsValid()
        {
            ValidationResult = new AddAddressCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class AddAddressCommandValidation : AbstractValidator<AddAddressCommand>
        {
            public AddAddressCommandValidation()
            {
                RuleFor(x => x.Street)
                    .NotEmpty()
                    .WithMessage("Nome da rua não pode ficar em branco!")
                    .MaximumLength(150)
                    .WithMessage("A rua deve conter no máximo 150 caracteres!");

                RuleFor(x => x.City)
                    .NotEmpty()
                    .WithMessage("Nome da cidade não pode ficar em branco!")
                    .MaximumLength(100)
                    .WithMessage("A cidade deve conter no máximo 100 caracteres!");

                RuleFor(x => x.State)
                    .NotEmpty()
                    .WithMessage("O estado não pode ficar em branco!")
                    .MaximumLength(2)
                    .WithMessage("O estado deve conter no máximo 2 caracteres!");

                RuleFor(x => x.Country)
                    .NotEmpty()
                    .WithMessage("O País não pode ficar em branco!")
                    .MaximumLength(50)
                    .WithMessage("O País deve conter no máximo 50 caracteres!");

                RuleFor(x => x.ZipCode)
                    .NotEmpty()
                    .WithMessage("Código postal não pode ficar em branco!")
                    .MaximumLength(20)
                    .WithMessage("O código postal deve conter no máximo 100 caracteres!");
            }
        }
    }
}
