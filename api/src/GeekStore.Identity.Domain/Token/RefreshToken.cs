using FluentValidation;
using GeekStore.Core.Models;

namespace GeekStore.Identity.Domain.Token
{
    public class RefreshToken : Entity
    {
        protected RefreshToken() { }
        public RefreshToken(string username, DateTime expirationDate)
        {
            Id = Guid.NewGuid();
            UserName = username;
            ExpirationDate = expirationDate;
        }

        public DateTime ExpirationDate { get; private set; }
        public string UserName { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new RefreshTokenEntityValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class RefreshTokenEntityValidation : AbstractValidator<RefreshToken>
        {
            public RefreshTokenEntityValidation()
            {
                RuleFor(r => r.ExpirationDate)
                    .NotEmpty()
                    .WithMessage("Data de expiração inválida!")
                    .GreaterThan(DateTime.Now)
                    .WithMessage("Data de expiração não pode ser retroativa!");

                RuleFor(r => r.UserName)
                    .NotEmpty()
                    .WithMessage("Nome do usuário não pode estar em branco!");
            }
        }
    }
}
