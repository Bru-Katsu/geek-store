using GeekStore.Cart.Application.Cart.Commands;

namespace GeekStore.Cart.Tests.Carts.Commands
{
    public class RemoveProductFromCartCommandTests
    {
        [Fact(DisplayName = "Comando de remoção de produto do carrinho deve ser válido")]
        [Trait("Commands", nameof(RemoveProductFromCartCommand))]
        public void RemoveProductFromCartCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var command = new RemoveProductFromCartCommand(Guid.NewGuid(), Guid.NewGuid());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o id do usuário é inválido")]
        [Trait("Commands", nameof(RemoveProductFromCartCommand))]
        public void RemoveProductFromCartCommand_ShouldHaveValidationError_WhenUserIdInvalid()
        {
            //Arrange
            var command = new RemoveProductFromCartCommand(Guid.Empty, Guid.NewGuid());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(RemoveProductFromCartCommand.UserId), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }


        [Fact(DisplayName = "Deve ter erro de validação quando o id do produto é inválido")]
        [Trait("Commands", nameof(RemoveProductFromCartCommand))]
        public void RemoveProductFromCartCommand_ShouldHaveValidationError_WhenProductIdInvalid()
        {
            //Arrange
            var command = new RemoveProductFromCartCommand(Guid.NewGuid(), Guid.Empty);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(RemoveProductFromCartCommand.ProductId), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }
    }
}
