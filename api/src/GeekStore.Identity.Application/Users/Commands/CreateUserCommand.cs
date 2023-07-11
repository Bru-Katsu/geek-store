using FluentValidation;
using GeekStore.Core.Messages;

namespace GeekStore.Identity.Application.Users.Commands
{
    public class CreateUserCommand : Command<bool>
    {
        public CreateUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }

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
            }
        }
    }
}
