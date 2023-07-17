using Bogus;
using GeekStore.Cart.Application.Cart.Commands;
using GeekStore.Cart.Tests.Common;

namespace GeekStore.Cart.Tests.Carts.Commands
{
    public class AddProductToCartCommandTests
    {
        private readonly Faker _faker;

        public AddProductToCartCommandTests()
        {
            _faker = new(Constants.LOCALE);
        }

        [Fact(DisplayName = "Comando de adicionar produtos ao carrinho deve ser válido")]
        [Trait("Commands", nameof(AddProductToCartCommand))]
        public void AddProductToCartCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var command = new AddProductToCartCommand(Guid.NewGuid(), Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Random.Number(min: 1), _faker.Random.Number(min: 1));

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o id do usuario é inválido")]
        [Trait("Commands", nameof(AddProductToCartCommand))]
        public void AddProductToCartCommand_ShouldHaveValidationError_WhenUserIdInvalid()
        {
            //Arrange
            var command = new AddProductToCartCommand(Guid.Empty, Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Random.Number(min: 1), _faker.Random.Number(min: 1));

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(AddProductToCartCommand.UserId), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o id do produto é inválido")]
        [Trait("Commands", nameof(AddProductToCartCommand))]
        public void AddProductToCartCommand_ShouldHaveValidationError_WhenProductIdInvalid()
        {
            //Arrange
            var command = new AddProductToCartCommand(Guid.NewGuid(), Guid.Empty, _faker.Commerce.ProductName(), _faker.Random.Number(min: 1), _faker.Random.Number(min: 1));

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(AddProductToCartCommand.ProductId), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o nome do produto está em branco")]
        [Trait("Commands", nameof(AddProductToCartCommand))]
        public void AddProductToCartCommand_ShouldHaveValidationError_WhenProductNameIsEmpty()
        {
            //Arrange
            var command = new AddProductToCartCommand(Guid.NewGuid(), Guid.NewGuid(), string.Empty, _faker.Random.Number(min: 1), _faker.Random.Number(min: 1));

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(AddProductToCartCommand.ProductName), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a quantidade do produto está zerada")]
        [Trait("Commands", nameof(AddProductToCartCommand))]
        public void AddProductToCartCommand_ShouldHaveValidationError_WhenProductQuantityIsZero()
        {
            //Arrange
            var command = new AddProductToCartCommand(Guid.NewGuid(), Guid.NewGuid(), _faker.Commerce.ProductName(), 0, _faker.Random.Number(min: 1));

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(AddProductToCartCommand.ProductQuantity), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a quantidade do produto é menor que zero")]
        [Trait("Commands", nameof(AddProductToCartCommand))]
        public void AddProductToCartCommand_ShouldHaveValidationError_WhenProductQuantityIsLessThanZero()
        {
            //Arrange
            var command = new AddProductToCartCommand(Guid.NewGuid(), Guid.NewGuid(), _faker.Commerce.ProductName(), -5, _faker.Random.Number(min: 1));

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(AddProductToCartCommand.ProductQuantity), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o preço do produto está zerado")]
        [Trait("Commands", nameof(AddProductToCartCommand))]
        public void AddProductToCartCommand_ShouldHaveValidationError_WhenProductPriceIsZero()
        {
            //Arrange
            var command = new AddProductToCartCommand(Guid.NewGuid(), Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Random.Number(min: 1), decimal.Zero);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(AddProductToCartCommand.ProductPrice), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o preço do produto é menor que zero")]
        [Trait("Commands", nameof(AddProductToCartCommand))]
        public void AddProductToCartCommand_ShouldHaveValidationError_WhenProductPriceIsLessThanZero()
        {
            //Arrange
            var command = new AddProductToCartCommand(Guid.NewGuid(), Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Random.Number(min: 1), -5);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(AddProductToCartCommand.ProductPrice), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }
    }
}
