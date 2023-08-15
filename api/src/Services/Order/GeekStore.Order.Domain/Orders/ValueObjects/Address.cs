using FluentValidation;
using FluentValidation.Results;
using GeekStore.Core.Models;
using System.Net;

namespace GeekStore.Order.Domain.Orders.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string city, string state, string country, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }

        public override ValidationResult Validate()
        {
            ValidationResult = new AddressValueObjectValidator().Validate(this);
            return ValidationResult;
        }

        private class AddressValueObjectValidator : AbstractValidator<Address>
        {
            public AddressValueObjectValidator()
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
