using Bogus;
using GeekStore.Identity.Application.Users.Commands;

namespace GeekStore.Identity.Tests.Users.Commands
{
    public class LoginCommandTests
    {
        private readonly Faker _faker;

        public LoginCommandTests()
        {
            _faker = new Faker();
        }

        [Fact(DisplayName = "Comando de login deve ser válido")]
        [Trait("Commands", nameof(LoginCommand))]
        public void LoginCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var command = new LoginCommand(_faker.Internet.Email(), _faker.Internet.Password());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o email é inválido")]
        [Trait("Commands", nameof(LoginCommand))]
        public void LoginCommand_ShouldHaveValidationError_WhenEmailIsInvalid()
        {
            //Arrange
            //Arrange
            string email = "email.invalido.com";
            string password = _faker.Internet.Password();

            var command = new LoginCommand(email, password);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Email)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a senha é nula ou branco")]
        [Trait("Commands", nameof(LoginCommand))]
        public void LoginCommand_ShouldHaveValidationError_WhenPasswordIsNullOrEmpty()
        {
            //Arrange
            //Arrange
            string email = _faker.Internet.Email();
            string password = string.Empty;

            var command = new LoginCommand(email, password);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Password)));
            Assert.Single(command.ValidationResult.Errors);
        }
    }
}
