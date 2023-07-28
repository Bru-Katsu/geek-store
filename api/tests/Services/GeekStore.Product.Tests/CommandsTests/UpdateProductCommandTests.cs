using Bogus;
using Bogus.Extensions;
using GeekStore.Product.Application.Products.Commands;

namespace GeekStore.Product.Tests.CommandsTests
{
    public class UpdateProductCommandTests
    {
        private readonly Faker _faker;

        public UpdateProductCommandTests()
        {
            _faker = new Faker();
        }

        [Fact(DisplayName = "Ao atualizar um produto, se o comando estiver válido, deve retornar como comando válido")]
        [Trait("Commands", "UpdateProductCommand")]
        public void UpdateProductCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string productName = _faker.Commerce.ProductName();
            string description = _faker.Commerce.ProductDescription();
            string category = _faker.Commerce.Categories(1).First();
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new UpdateProductCommand(id, productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.False(command.ValidationResult.Errors.Any());            
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o id é nulo ou vazio")]
        [Trait("Commands", "UpdateProductCommand")]
        public void UpdateProductCommand_ShouldHaveValidationError_WhenIdIsNullOrEmpty()
        {
            //Arrange
            Guid id = Guid.Empty;

            string productName = _faker.Commerce.ProductName();
            string description = _faker.Commerce.ProductDescription();
            string category = _faker.Commerce.Categories(1).First();
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new UpdateProductCommand(id, productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(UpdateProductCommand.Id)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o nome é nulo ou vazio")]
        [Trait("Commands", "UpdateProductCommand")]
        public void UpdateProductCommand_ShouldHaveValidationError_WhenNameIsNullOrEmpty()
        {
            //Arrange
            string productName = null;

            Guid id = Guid.NewGuid();
            string description = _faker.Commerce.ProductDescription();
            string category = _faker.Commerce.Categories(1).First();
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new UpdateProductCommand(id, productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(UpdateProductCommand.Name)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o nome excede o tamanho máximo")]
        [Trait("Commands", "UpdateProductCommand")]
        public void UpdateProductCommand_ShouldHaveValidationError_WhenNameExceedsMaxLength()
        {
            //Arrange
            string productName = _faker.Commerce.ProductName().ClampLength(151);

            Guid id = Guid.NewGuid();
            string description = _faker.Commerce.ProductDescription().ClampLength(500);
            string category = _faker.Commerce.Categories(1).First().ClampLength(50);
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new UpdateProductCommand(id, productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(UpdateProductCommand.Name)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o preço é zero ou negativo")]
        [Trait("Commands", "UpdateProductCommand")]
        public void UpdateProductCommand_ShouldHaveValidationError_WhenPriceIsZeroOrNegative()
        {
            //Arrange
            decimal price = decimal.Parse(_faker.Commerce.Price(-50, -1));

            Guid id = Guid.NewGuid();
            string productName = _faker.Commerce.ProductName().ClampLength(150);
            string description = _faker.Commerce.ProductDescription().ClampLength(500);
            string category = _faker.Commerce.Categories(1).First().ClampLength(50);
            string imageUlr = "https://example.com/image.jpg";

            var command = new UpdateProductCommand(id, productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(UpdateProductCommand.Price)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a descrição excede o tamanho máximo")]
        [Trait("Commands", "UpdateProductCommand")]
        public void UpdateProductCommand_ShouldHaveValidationError_WhenDescriptionExceedsMaxLength()
        {
            //Arrange
            string description = _faker.Commerce.ProductDescription().ClampLength(501);

            Guid id = Guid.NewGuid();
            string productName = _faker.Commerce.ProductName().ClampLength(150);
            string category = _faker.Commerce.Categories(1).First().ClampLength(50);
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new UpdateProductCommand(id, productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(UpdateProductCommand.Description)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a categoria é nula ou vazia")]
        [Trait("Commands", "UpdateProductCommand")]
        public void UpdateProductCommand_ShouldHaveValidationError_WhenCategoryIsNullOrEmpty()
        {
            //Arrange
            string category = string.Empty;

            Guid id = Guid.NewGuid();
            string productName = _faker.Commerce.ProductName().ClampLength(150);
            string description = _faker.Commerce.ProductDescription().ClampLength(500);
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new UpdateProductCommand(id, productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(UpdateProductCommand.Category)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a categoria excede o tamanho máximo")]
        [Trait("Commands", "UpdateProductCommand")]
        public void UpdateProductCommand_ShouldHaveValidationError_WhenCategoryExceedsMaxLength()
        {
            //Arrange
            string category = _faker.Commerce.ProductName().ClampLength(51);

            Guid id = Guid.NewGuid();
            string productName = _faker.Commerce.ProductName().ClampLength(150);
            string description = _faker.Commerce.ProductDescription().ClampLength(500);
            string imageUlr = "https://example.com/image.jpg";
            decimal price = decimal.Parse(_faker.Commerce.Price());

            var command = new UpdateProductCommand(id, productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(UpdateProductCommand.Category)));
            Assert.Single(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando a URL da imagem é inválida")]
        [Trait("Commands", "UpdateProductCommand")]
        public void UpdateProductCommand_ShouldHaveValidationError_WhenImageURLIsInvalid()
        {
            //Arrange
            string productName = _faker.Commerce.ProductName().ClampLength(150);
            string description = _faker.Commerce.ProductDescription().ClampLength(500);
            string category = _faker.Commerce.ProductName().ClampLength(50);
            string imageUlr = "invalid-url";

            decimal price = decimal.Parse(_faker.Commerce.Price());
            Guid id = Guid.NewGuid();

            var command = new UpdateProductCommand(id, productName, price, description, category, imageUlr);

            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.False(isValid);
            Assert.Contains(command.ValidationResult.Errors, e => e.PropertyName.Equals(nameof(UpdateProductCommand.ImageURL)));
            Assert.Single(command.ValidationResult.Errors);
        }
    }
}
