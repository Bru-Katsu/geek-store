using GeekStore.Cart.Application.Cart.Commands;

namespace GeekStore.Cart.Tests.Carts.Commands
{
    public class RemoveCouponCartCommandTests
    {
        [Fact(DisplayName = "Comando de remoção de cupom do carrinho deve ser válido")]
        [Trait("Commands", nameof(RemoveCouponCartCommand))]
        public void RemoveCouponCartCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var command = new RemoveCouponCartCommand(Guid.NewGuid());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o id do usuário é inválido")]
        [Trait("Commands", nameof(RemoveCouponCartCommand))]
        public void RemoveCouponCartCommand_ShouldHaveValidationError_WhenUserIdInvalid()
        {
            //Arrange
            var command = new RemoveCouponCartCommand(Guid.Empty);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(RemoveCouponCartCommand.UserId), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }
    }
}
