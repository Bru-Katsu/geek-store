using Bogus;
using Bogus.Extensions;
using GeekStore.Product.Application.Products.Commands;

namespace GeekStore.Product.Tests.CommandsTests
{
    public class AddProductCommandTests
    {
        private readonly Faker _faker;

        public AddProductCommandTests()
        {
            _faker = new Faker();
        }

        [Fact(DisplayName = "Ao adicionar um produto, se o comando estiver válido, deve retornar como comando válido")]
        [Trait("Commands", "AddProductCommand")]
        public void AddProductCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            string productName = _faker.Commerce.ProductName();
            string description = _faker.Commerce.ProductDescription();
            string category = _faker.Commerce.Categories(1).First();
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new AddProductCommand(productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o nome é nulo ou vazio")]
        [Trait("Commands", "AddProductCommand")]
        public void AddProductCommand_ShouldHaveValidationError_WhenNameIsNullOrEmpty()
        {
            //Arrange
            //Arrange
            string productName = null;

            string description = _faker.Commerce.ProductDescription();
            string category = _faker.Commerce.Categories(1).First();
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new AddProductCommand(productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(AddProductCommand.Name)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o nome excede o tamanho máximo")]
        [Trait("Commands", "AddProductCommand")]
        public void AddProductCommand_ShouldHaveValidationError_WhenNameExceedsMaxLength()
        {
            //Arrange
            string productName = _faker.Commerce.ProductName().ClampLength(151);

            string description = _faker.Commerce.ProductDescription().ClampLength(500);
            string category = _faker.Commerce.Categories(1).First().ClampLength(50);
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new AddProductCommand(productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(AddProductCommand.Name)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o preço é zero ou negativo")]
        [Trait("Commands", "AddProductCommand")]
        public void AddProductCommand_ShouldHaveValidationError_WhenPriceIsZeroOrNegative()
        {
            //Arrange
            decimal price = decimal.Parse(_faker.Commerce.Price(-50, -1));

            string productName = _faker.Commerce.ProductName().ClampLength(150);
            string description = _faker.Commerce.ProductDescription().ClampLength(500);
            string category = _faker.Commerce.Categories(1).First().ClampLength(50);
            string imageUlr = "https://example.com/image.jpg";

            var command = new AddProductCommand(productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(AddProductCommand.Price)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a descrição excede o tamanho máximo")]
        [Trait("Commands", "AddProductCommand")]
        public void AddProductCommand_ShouldHaveValidationError_WhenDescriptionExceedsMaxLength()
        {
            //Arrange
            string description = _faker.Commerce.ProductDescription().ClampLength(501);

            string productName = _faker.Commerce.ProductName().ClampLength(150);
            string category = _faker.Commerce.Categories(1).First().ClampLength(50);
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new AddProductCommand(productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(AddProductCommand.Description)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a categoria é nula ou vazia")]
        [Trait("Commands", "AddProductCommand")]
        public void AddProductCommand_ShouldHaveValidationError_WhenCategoryIsNullOrEmpty()
        {
            //Arrange
            string category = string.Empty;

            string productName = _faker.Commerce.ProductName().ClampLength(150);
            string description = _faker.Commerce.ProductDescription().ClampLength(500);
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new AddProductCommand(productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(AddProductCommand.Category)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a categoria excede o tamanho máximo")]
        [Trait("Commands", "AddProductCommand")]
        public void AddProductCommand_ShouldHaveValidationError_WhenCategoryExceedsMaxLength()
        {
            //Arrange
            string category = _faker.Commerce.ProductName().ClampLength(51);

            string productName = _faker.Commerce.ProductName().ClampLength(150);
            string description = _faker.Commerce.ProductDescription().ClampLength(500);
            string imageUlr = "https://example.com/image.jpg".ClampLength(300);
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new AddProductCommand(productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(AddProductCommand.Category)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a URL da imagem é inválida")]
        [Trait("Commands", "AddProductCommand")]
        public void AddProductCommand_ShouldHaveValidationError_WhenImageURLIsInvalid()
        {
            //Arrange
            decimal price = decimal.Parse(_faker.Commerce.Price());

            string productName = _faker.Commerce.ProductName().ClampLength(150);
            string description = _faker.Commerce.ProductDescription().ClampLength(500);
            string category = _faker.Commerce.ProductName().ClampLength(50);
            string imageUlr = "invalid-url";

            var command = new AddProductCommand(productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(AddProductCommand.ImageURL)));
            Assert.Single(command.ValidationResult.Errors);
        }
    }
}
