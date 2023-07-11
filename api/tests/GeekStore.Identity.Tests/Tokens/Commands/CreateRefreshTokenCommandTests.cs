using Bogus;
using GeekStore.Identity.Application.Tokens.Commands;

namespace GeekStore.Identity.Tests.Tokens.Commands
{
    public class CreateRefreshTokenCommandTests
    {
        private readonly Faker _faker;

        public CreateRefreshTokenCommandTests()
        {
            _faker = new Faker();
        }

        [Fact(DisplayName = "Comando de criação de refresh token deve ser válido")]
        [Trait("Commands", "CreateRefreshTokenCommand")]
        public void CreateRefreshTokenCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var command = new CreateRefreshTokenCommand(_faker.Internet.Email());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o email é inválido")]
        [Trait("Commands", "CreateRefreshTokenCommand")]
        public void CreateRefreshTokenCommand_ShouldHaveValidationError_WhenEmailIsInvalid()
        {
            //Arrange
            //Arrange
            string email = "email.invalido.com";

            var command = new CreateRefreshTokenCommand(email);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateRefreshTokenCommand.Email)));
            Assert.Single(command.ValidationResult.Errors);
        }
    }
}
