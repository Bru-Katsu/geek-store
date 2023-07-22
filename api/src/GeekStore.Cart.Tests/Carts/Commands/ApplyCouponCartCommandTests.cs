using GeekStore.Cart.Application.Cart.Commands;

namespace GeekStore.Cart.Tests.Carts.Commands
{
    public class ApplyCouponCartCommandTests
    {
        [Fact(DisplayName = "Comando de aplicar cupom de descontos do carrinho deve ser válido")]
        [Trait("Commands", nameof(ApplyCouponCartCommand))]
        public void ApplyCouponCartCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var command = new ApplyCouponCartCommand(Guid.NewGuid(), Guid.NewGuid());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o id do usuário é inválido")]
        [Trait("Commands", nameof(ApplyCouponCartCommand))]
        public void ApplyCouponCartCommand_ShouldHaveValidationError_WhenUserIdInvalid()
        {
            //Arrange
            var command = new ApplyCouponCartCommand(Guid.Empty, Guid.NewGuid());

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(ApplyCouponCartCommand.UserId), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }


        [Fact(DisplayName = "Deve ter erro de validação quando o id do cupom é inválido")]
        [Trait("Commands", nameof(ApplyCouponCartCommand))]
        public void ApplyCouponCartCommand_ShouldHaveValidationError_WhenCouponIdInvalid()
        {
            //Arrange
            var command = new ApplyCouponCartCommand(Guid.NewGuid(), Guid.Empty);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(ApplyCouponCartCommand.CouponId), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }
    }
}
