using FluentValidation;
using GeekStore.Core.Messages;
using GeekStore.Core.Results;
using GeekStore.Identity.Application.Tokens.ViewModels;

namespace GeekStore.Identity.Application.Tokens.Commands
{
    public class GenerateTokenCommand : Command<Result<TokenResponseViewModel>>
    {
        public GenerateTokenCommand(string email, Guid refreshTokenID)
        {
            Email = email;
            RefreshTokenId = refreshTokenID;
        }

        public string Email { get; }
        public Guid RefreshTokenId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new GenerateTokenCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class GenerateTokenCommandValidation : AbstractValidator<GenerateTokenCommand>
        {
            public GenerateTokenCommandValidation()
            {
                RuleFor(x => x.Email)
                    .EmailAddress()
                    .WithMessage("Email inválido!");

                RuleFor(x => x.RefreshTokenId)
                    .NotEmpty()
                    .WithMessage("Refresh Token inválido!");
            }
        }
    }
}
