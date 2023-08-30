using FluentValidation;
using GeekStore.Core.Messages;

namespace GeekStore.Identity.Application.Users.Commands
{
    public class DeleteUserByEmailCommand : Command<bool>
    {
        public DeleteUserByEmailCommand(string email)
        {
            Email = email;
        }

        public string Email { get; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteUserByEmailCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class DeleteUserByEmailCommandValidation : AbstractValidator<DeleteUserByEmailCommand>
        {
            public DeleteUserByEmailCommandValidation()
            {
                RuleFor(x => x.Email)
                    .EmailAddress()
                    .WithMessage("Email inválido!");
            }
        }
    }
}
