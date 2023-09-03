using Bogus;
using Bogus.Extensions.Brazil;
using GeekStore.Customer.Application.Customers.Commands;

namespace GeekStore.Customer.Tests.Customers.Commands
{
    public class CreateCustomerCommandTests
    {
        private readonly Faker _faker;

        public CreateCustomerCommandTests()
        {
            _faker = new();
        }

        [Fact(DisplayName = "Comando de criar cliente deve ser válido")]
        [Trait("Commands", nameof(CreateCustomerCommand))]
        public void CreateCustomerCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var name = _faker.Name.FirstName();
            var surname = _faker.Name.LastName();
            var birthday = _faker.Date.Past();
            var document = _faker.Person.Cpf();
            var email = _faker.Person.Email;

            var command = new CreateCustomerCommand(userId, name, surname, birthday, document, email);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.Empty(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o id de usuário é inválido")]
        [Trait("Commands", nameof(CreateCustomerCommand))]
        public void CreateCustomerCommand_ShouldHaveValidationError_WhenInvalidUserId()
        {
            //Arrange
            var userId = Guid.Empty;
            var name = _faker.Name.FirstName();
            var surname = _faker.Name.LastName();
            var birthday = _faker.Date.Past();
            var document = _faker.Person.Cpf();
            var email = _faker.Person.Email;

            var command = new CreateCustomerCommand(userId, name, surname, birthday, document, email);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(CreateCustomerCommand.UserId), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o nome está em branco")]
        [Trait("Commands", nameof(CreateCustomerCommand))]
        public void CreateCustomerCommand_ShouldHaveValidationError_WhenNameIsEmpty()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var name = string.Empty;
            var surname = _faker.Name.LastName();
            var birthday = _faker.Date.Past();
            var document = _faker.Person.Cpf();
            var email = _faker.Person.Email;

            var command = new CreateCustomerCommand(userId, name, surname, birthday, document, email);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(CreateCustomerCommand.Name), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o sobrenome está em branco")]
        [Trait("Commands", nameof(CreateCustomerCommand))]
        public void CreateCustomerCommand_ShouldHaveValidationError_WhenSurnameIsEmpty()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var name = _faker.Name.FirstName();
            var surname = string.Empty;
            var birthday = _faker.Date.Past();
            var document = _faker.Person.Cpf();
            var email = _faker.Person.Email;

            var command = new CreateCustomerCommand(userId, name, surname, birthday, document, email);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(CreateCustomerCommand.Surname), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a data de nascimento está no futuro")]
        [Trait("Commands", nameof(CreateCustomerCommand))]
        public void CreateCustomerCommand_ShouldHaveValidationError_WhenBirthdayInFuture()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var name = _faker.Name.FirstName();
            var surname = _faker.Name.LastName();
            var birthday = DateTime.Now.AddDays(1);
            var document = _faker.Person.Cpf();
            var email = _faker.Person.Email;

            var command = new CreateCustomerCommand(userId, name, surname, birthday, document, email);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(CreateCustomerCommand.Birthday), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o documento está vazio")]
        [Trait("Commands", nameof(CreateCustomerCommand))]
        public void CreateCustomerCommand_ShouldHaveValidationError_WhenDocumentIsEmpty()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var name = _faker.Name.FirstName();
            var surname = _faker.Name.LastName();
            var birthday = _faker.Date.Past();
            var document = string.Empty;
            var email = _faker.Person.Email;

            var command = new CreateCustomerCommand(userId, name, surname, birthday, document, email);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(CreateCustomerCommand.Document), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o email está inválido")]
        [Trait("Commands", nameof(CreateCustomerCommand))]
        public void CreateCustomerCommand_ShouldHaveValidationError_WhenEmailIsInvalid()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var name = _faker.Name.FirstName();
            var surname = _faker.Name.LastName();
            var birthday = _faker.Date.Past();
            var document = _faker.Person.Cpf();
            var email = "email.invalido.com";

            var command = new CreateCustomerCommand(userId, name, surname, birthday, document, email);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(CreateCustomerCommand.Email), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }
    }
}
