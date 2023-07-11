using FluentValidation;
using GeekStore.Core.Messages;
using GeekStore.Core.Results;

namespace GeekStore.Identity.Application.Tokens.Commands
{
    public class CreateRefreshTokenCommand : Command<Result<Guid>>
    {
        public CreateRefreshTokenCommand(string email)
        {
            Email = email;
        }

        public string Email { get; }

        public override bool IsValid()
        {
            ValidationResult = new CreateRefreshTokenCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class CreateRefreshTokenCommandValidation : AbstractValidator<CreateRefreshTokenCommand>
        {
            public CreateRefreshTokenCommandValidation()
            {
                RuleFor(x => x.Email)
                    .EmailAddress()
                    .WithMessage("Email inválido!");
            }
        }
    }
}
