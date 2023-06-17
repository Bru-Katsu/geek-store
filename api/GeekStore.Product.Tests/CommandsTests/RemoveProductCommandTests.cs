using Bogus;
using GeekStore.Product.Application.Products.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekStore.Product.Tests.CommandsTests
{
    public class RemoveProductCommandTests
    {
        private readonly Faker _faker;

        public RemoveProductCommandTests()
        {
            _faker = new Faker();
        }

        [Fact(DisplayName = "Ao remover um produto, se o comando estiver válido, deve retornar como comando válido")]
        [Trait("Commands", "RemoveProductCommand")]
        public void AddProductCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var id = Guid.NewGuid();
            var command = new RemoveProductCommand(id);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o id é nulo ou vazio")]
        [Trait("Commands", "RemoveProductCommand")]
        public void UpdateProductCommand_ShouldHaveValidationError_WhenIdIsNullOrEmpty()
        {
            //Arrange
            Guid id = Guid.Empty;

            var command = new RemoveProductCommand(id);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(UpdateProductCommand.Id)));
            Assert.Single(command.ValidationResult.Errors);
        }
    }
}
