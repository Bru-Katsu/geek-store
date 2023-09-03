using Bogus;
using GeekStore.Customer.Application.Customers.Commands;
using System.Security.AccessControl;

namespace GeekStore.Customer.Tests.Customers.Commands
{
    public class ChangeProfileImageCommandTests
    {
        private readonly Faker _faker;

        public ChangeProfileImageCommandTests()
        {
            _faker = new();
        }

        [Fact(DisplayName = "Comando de alterar imagem de perfil do cliente deve ser válido")]
        [Trait("Commands", nameof(ChangeProfileImageCommand))]
        public void ChangeProfileImageCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var command = new ChangeProfileImageCommand(Guid.NewGuid(), _faker.Image.LoremFlickrUrl());

            //Act
            var result = command.IsValid();

            //Assert
            Assert.True(result);
            Assert.Empty(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o id do cliente é inválido")]
        [Trait("Commands", nameof(ChangeProfileImageCommand))]
        public void ChangeProfileImageCommand_ShouldBeInvalid_WhenIdIsInvalid()
        {
            //Arrange
            var command = new ChangeProfileImageCommand(Guid.Empty, _faker.Image.LoremFlickrUrl());

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(ChangeProfileImageCommand.Id), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o link da imagem de perfil está em branco")]
        [Trait("Commands", nameof(ChangeProfileImageCommand))]
        public void ChangeProfileImageCommand_ShouldBeInvalid_WhenImageLinkIsEmpty()
        {
            //Arrange
            var command = new ChangeProfileImageCommand(Guid.NewGuid(), string.Empty);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(ChangeProfileImageCommand.ProfileImage), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }
    }
}
