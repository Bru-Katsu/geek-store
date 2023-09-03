using FluentValidation;
using GeekStore.Core.Messages;
using GeekStore.Core.Results;

namespace GeekStore.Customer.Application.Customers.Commands
{
    public class CreateCustomerCommand : Command<Result<Guid>>
    {
        public CreateCustomerCommand(Guid userId, string name, string surname, DateTime birthday, string document, string email)
        {
            UserId = userId;
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Document = document;
            Email = email;
        }

        public Guid UserId { get; }
        public string Name { get; }
        public string Surname { get; }
        public DateTime Birthday { get; }
        public string Document { get; }
        public string Email { get; }

        public override bool IsValid()
        {
            ValidationResult = new CreateCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class CreateCustomerCommandValidation : AbstractValidator<CreateCustomerCommand> 
        {
            public CreateCustomerCommandValidation()
            {
                RuleFor(x => x.UserId)
                    .NotEmpty()
                    .WithMessage("Id de usuário inválido!");

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Nome não pode ficar vazio!");

                RuleFor(x => x.Surname)
                    .NotEmpty()
                    .WithMessage("Sobrenome não pode ficar vazio!");

                RuleFor(x => x.Birthday)
                    .LessThanOrEqualTo(DateTime.Now)
                    .WithMessage("Data de nascimento inválida!");

                RuleFor(x => x.Document)
                    .NotEmpty()
                    .WithMessage("Infome o documento de identificação!");

                RuleFor(x => x.Email)
                    .EmailAddress()
                    .WithMessage("Endereço de e-mail inválido!")
                    .NotEmpty()
                    .WithMessage("O e-mail não pode ficar em branco!");
            }
        }
    }
}
