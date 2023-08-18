using FluentValidation;
using GeekStore.Core.Models;
using GeekStore.Customer.Domain.Customers.Enums;

namespace GeekStore.Customer.Domain.Customers
{
    public class CustomerAddress : Entity
    {
        public CustomerAddress(string street, string city, string state, string country, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;

            Type = AddressType.Common;
        }

        public Customer Customer { get; private set; }
        public Guid CustomerId { get; private set; }

        public string Street { get; private set; }
        public void ChangeStreet(string street)
        {
            Street = street;
        }

        public string City { get; private set; }
        public void ChangeCity(string city)
        {
            City = city;
        }

        public string State { get; private set; }
        public void ChangeState(string state)
        {
            State = state;
        }

        public string Country { get; private set; }
        public void ChangeCountry(string country)
        {
            Country = country;
        }

        public string ZipCode { get; private set; }
        public void ChangeZipCode(string zipCode)
        {
            ZipCode = zipCode;
        }

        public AddressType Type { get; private set; }
        public void DefineAsShipping()
        {
            Type = AddressType.Shipping;
        }

        public void DefineAsChargePayment()
        {
            Type = AddressType.ChargePayment;
        }

        public void DefineAsCommon()
        {
            Type = AddressType.Common;
        }

        public override bool IsValid()
        {
            ValidationResult = new CustomerAddressEntityValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class CustomerAddressEntityValidation : AbstractValidator<CustomerAddress>
        {
            public CustomerAddressEntityValidation()
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
