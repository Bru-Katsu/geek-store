using FluentValidation;
using GeekStore.Core.Messages;

namespace GeekStore.Identity.Application.Users.Commands
{
    public class LoginCommand : Command<bool>
    {
        public LoginCommand(string username, string password)
        {
            Email = username;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }

        public override bool IsValid()
        {
            ValidationResult = new LoginCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        private class LoginCommandValidator : AbstractValidator<LoginCommand>
        {
            public LoginCommandValidator()
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
