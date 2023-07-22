using GeekStore.Cart.Application.Cart.Commands;

namespace GeekStore.Cart.Tests.Carts.Commands
{
    public class RemoveProductCartCommandTests
    {
        [Fact(DisplayName = "Comando de remoção de produto do carrinho deve ser válido")]
        [Trait("Commands", nameof(RemoveProductCartCommand))]
        public void RemoveProductCartCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var command = new RemoveProductCartCommand(Guid.NewGuid(), Guid.NewGuid());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o id do usuário é inválido")]
        [Trait("Commands", nameof(RemoveProductCartCommand))]
        public void RemoveProductCartCommand_ShouldHaveValidationError_WhenUserIdInvalid()
        {
            //Arrange
            var command = new RemoveProductCartCommand(Guid.Empty, Guid.NewGuid());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(RemoveProductCartCommand.UserId), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }


        [Fact(DisplayName = "Deve ter erro de validação quando o id do produto é inválido")]
        [Trait("Commands", nameof(RemoveProductCartCommand))]
        public void RemoveProductCartCommand_ShouldHaveValidationError_WhenProductIdInvalid()
        {
            //Arrange
            var command = new RemoveProductCartCommand(Guid.NewGuid(), Guid.Empty);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(RemoveProductCartCommand.ProductId), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }
    }
}
