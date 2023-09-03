using Bogus;
using Bogus.Extensions;
using Bogus.Extensions.Brazil;
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
            var command = new CreateUserCommand(_faker.Internet.Email(), _faker.Internet.Password(), _faker.Person.FirstName, _faker.Person.LastName, _faker.Person.Cpf(), _faker.Person.DateOfBirth);


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
            string email = "email.invalido.com";
            string password = _faker.Internet.Password();            
            
            var command = new CreateUserCommand(email, password, _faker.Person.FirstName, _faker.Person.LastName, _faker.Person.Cpf(), _faker.Person.DateOfBirth);

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
            string email = _faker.Internet.Email();
            string password = string.Empty;
            string name = _faker.Person.FirstName;
            string surname = _faker.Person.LastName;
            string document = _faker.Person.Cpf();
            DateTime birthday = _faker.Person.DateOfBirth;


            var command = new CreateUserCommand(email, password, name, surname, document, birthday);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Password)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o nome é nulo ou branco")]
        [Trait("Commands", "CreateUserCommand")]
        public void CreateUserCommand_ShouldHaveValidationError_WhenNameIsNullOrEmpty()
        {
            //Arrange
            string email = _faker.Internet.Email();
            string password = _faker.Internet.Password();
            string name = string.Empty;
            string surname = _faker.Person.LastName;
            string document = _faker.Person.Cpf();
            DateTime birthday = _faker.Person.DateOfBirth;

            var command = new CreateUserCommand(email, password, name, surname, document, birthday);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Name)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o nome é maior que 255 caracteres")]
        [Trait("Commands", "CreateUserCommand")]
        public void CreateUserCommand_ShouldHaveValidationError_WhenNameIsGreatherThanLimit()
        {
            //Arrange
            string email = _faker.Internet.Email();
            string password = _faker.Internet.Password();
            string name = _faker.Person.FirstName.ClampLength(256);
            string surname = _faker.Person.LastName;
            string document = _faker.Person.Cpf();
            DateTime birthday = _faker.Person.DateOfBirth;

            var command = new CreateUserCommand(email, password, name, surname, document, birthday);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Name)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o sobrenome é nulo ou branco")]
        [Trait("Commands", "CreateUserCommand")]
        public void CreateUserCommand_ShouldHaveValidationError_WhenSurnameIsNullOrEmpty()
        {
            //Arrange
            string email = _faker.Internet.Email();
            string password = _faker.Internet.Password();
            string name = _faker.Person.FirstName;
            string surname = string.Empty;
            string document = _faker.Person.Cpf();
            DateTime birthday = _faker.Person.DateOfBirth;

            var command = new CreateUserCommand(email, password, name, surname, document, birthday);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Surname)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o sobrenome é maior que 255 caracteres")]
        [Trait("Commands", "CreateUserCommand")]
        public void CreateUserCommand_ShouldHaveValidationError_WhenSurnameIsGreatherThanLimit()
        {
            //Arrange
            string email = _faker.Internet.Email();
            string password = _faker.Internet.Password();
            string name = _faker.Person.FirstName;
            string surname = _faker.Person.LastName.ClampLength(256);
            string document = _faker.Person.Cpf();
            DateTime birthday = _faker.Person.DateOfBirth;

            var command = new CreateUserCommand(email, password, name, surname, document, birthday);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Surname)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o documento de identificação é nulo ou branco")]
        [Trait("Commands", "CreateUserCommand")]
        public void CreateUserCommand_ShouldHaveValidationError_WhenDocumentIsNullOrEmpty()
        {
            //Arrange
            string email = _faker.Internet.Email();
            string password = _faker.Internet.Password();
            string name = _faker.Person.FirstName;
            string surname = _faker.Person.LastName;
            string document = string.Empty;
            DateTime birthday = _faker.Person.DateOfBirth;

            var command = new CreateUserCommand(email, password, name, surname, document, birthday);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Document)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o documento é maior que 100 caracteres")]
        [Trait("Commands", "CreateUserCommand")]
        public void CreateUserCommand_ShouldHaveValidationError_WhenDocumentIsGreatherThanLimit()
        {
            //Arrange
            string email = _faker.Internet.Email();
            string password = _faker.Internet.Password();
            string name = _faker.Person.FirstName;
            string surname = _faker.Person.LastName;
            string document = _faker.Person.Cpf().ClampLength(101);
            DateTime birthday = _faker.Person.DateOfBirth;

            var command = new CreateUserCommand(email, password, name, surname, document, birthday);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Document)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o a data de nascimento é maior que a data atual")]
        [Trait("Commands", "CreateUserCommand")]
        public void CreateUserCommand_ShouldHaveValidationError_WhenBirthdayIsGreaterThanNow()
        {
            //Arrange
            string email = _faker.Internet.Email();
            string password = _faker.Internet.Password();
            string name = _faker.Person.FirstName;
            string surname = _faker.Person.LastName;
            string document = _faker.Person.Cpf();
            DateTime birthday = DateTime.Now.AddDays(1);

            var command = new CreateUserCommand(email, password, name, surname, document, birthday);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(CreateUserCommand.Birthday)));
            Assert.Single(command.ValidationResult.Errors);
        }
    }
}
