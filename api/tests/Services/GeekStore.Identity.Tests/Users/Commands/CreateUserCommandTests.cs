using Bogus;
using GeekStore.Identity.Application.Users.Commands;

namespace GeekStore.Identity.Tests.Users.Commands
{
    public class CreateUserCommandTests
    {
        private readonly Faker _faker;

        public CreateUserCommandTests()
        {
            _faker = new Faker();
        }

        [Fact(DisplayName = "Comando de criação de novo usuário deve ser válido")]
        [Trait("Commands", "CreateUserCommand")]
        public void CreateUserCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var command = new CreateUserCommand(_faker.Internet.Email(), _faker.Internet.Password());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o email é inválido")]
        [Trait("Commands", "CreateUserCommand")]
        public void CreateUserCommand_ShouldHaveValidationError_WhenEmailIsInvalid()
        {
            //Arrange
            //Arrange
            string email = "email.invalido.com";
            string password = _faker.Internet.Password();            

            var command = new CreateUserCommand(email, password);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Email)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a senha é nula ou branco")]
        [Trait("Commands", "CreateUserCommand")]
        public void CreateUserCommand_ShouldHaveValidationError_WhenPasswordIsNullOrEmpty()
        {
            //Arrange
            //Arrange
            string email = _faker.Internet.Email();
            string password = string.Empty;

            var command = new CreateUserCommand(email, password);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Password)));
            Assert.Single(command.ValidationResult.Errors);
        }
    }
}
