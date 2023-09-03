using FluentValidation;
using GeekStore.Core.Messages;

namespace GeekStore.Identity.Application.Users.Commands
{
    public class CreateUserCommand : Command<bool>
    {
        public CreateUserCommand(string email, string password, string name, string surname, string document, DateTime birthday)
        {
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            Document = document;
            Birthday = birthday;
        }

        public string Email { get; }
        public string Password { get; }

        public string Name { get; }
        public string Surname { get; }
        public string Document { get; }
        public DateTime Birthday { get; }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
        {
            public CreateUserCommandValidation()
            {
                RuleFor(x => x.Email)
                    .EmailAddress()
                    .WithMessage("Email inválido!");

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Senha inválida!");

                RuleFor(x => x.Name)
                  .NotEmpty()
                  .WithMessage("Nome não pode ficar vazio!")
                  .MaximumLength(255)
                  .WithMessage("O nome deve conter no máximo 255 caracteres!"); ;

                RuleFor(x => x.Surname)
                    .NotEmpty()
                    .WithMessage("Sobrenome não pode ficar vazio!")
                    .MaximumLength(255)
                    .WithMessage("O sobrenome deve conter no máximo 255 caracteres!");

                RuleFor(x => x.Birthday)
                    .LessThanOrEqualTo(DateTime.Now)
                    .WithMessage("Data de nascimento inválida!");

                RuleFor(x => x.Document)
                    .NotEmpty()
                    .WithMessage("Infome o documento de identificação!")
                    .MaximumLength(100)
                    .WithMessage("O documento de identificação deve conter no máximo 100 caracteres!");
            }
        }
    }
}
