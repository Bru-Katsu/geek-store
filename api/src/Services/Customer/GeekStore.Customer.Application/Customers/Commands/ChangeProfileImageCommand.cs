using FluentValidation;
using GeekStore.Core.Messages;

namespace GeekStore.Customer.Application.Customers.Commands
{
    public class ChangeProfileImageCommand : Command
    {
        public ChangeProfileImageCommand(Guid id, string profileImage)
        {
            Id = id;
            ProfileImage = profileImage;
        }

        public Guid Id { get; }
        public string ProfileImage { get; }

        public override bool IsValid()
        {
            ValidationResult = new ChangeProfileImageCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class ChangeProfileImageCommandValidation : AbstractValidator<ChangeProfileImageCommand>
        {
            public ChangeProfileImageCommandValidation()
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id inválido!");

                RuleFor(x => x.ProfileImage)
                    .NotEmpty()
                    .WithMessage("Link de imagem inválido!");
            }
        }
    }
}
