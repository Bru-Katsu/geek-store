using Bogus;
using GeekStore.Identity.Application.Tokens.Commands;

namespace GeekStore.Identity.Tests.Tokens.Commands
{
    public class GenerateTokenCommandTests
    {
        private readonly Faker _faker;

        public GenerateTokenCommandTests()
        {
            _faker = new Faker();
        }

        [Fact(DisplayName = "Comando de criação de refresh token deve ser válido")]
        [Trait("Commands", nameof(GenerateTokenCommand))]
        public void GenerateTokenCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var command = new GenerateTokenCommand(_faker.Internet.Email(), Guid.NewGuid());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o email é inválido")]
        [Trait("Commands", nameof(GenerateTokenCommand))]
        public void GenerateTokenCommand_ShouldHaveValidationError_WhenEmailIsInvalid()
        {
            //Arrange
            string email = "email.invalido.com";

            var command = new GenerateTokenCommand(email, Guid.NewGuid());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(GenerateTokenCommand.Email)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o ID do refresh token é inválido")]
        [Trait("Commands", nameof(GenerateTokenCommand))]
        public void GenerateTokenCommand_ShouldHaveValidationError_WhenRefreshTokenIdIsInvalid()
        {
            //Arrange
            var command = new GenerateTokenCommand(_faker.Internet.Email(), Guid.Empty);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(GenerateTokenCommand.RefreshTokenId)));
            Assert.Single(command.ValidationResult.Errors);
        }
    }
}
