using Bogus;
using Bogus.Extensions;
using GeekStore.Product.Tests.Common;
using GeekStore.Product.Tests.Fixtures;

namespace GeekStore.Product.Tests.EntitiesTests
{
    public class ProductEntityTests : IClassFixture<ProductFixture>
    {
        private readonly ProductFixture _productFixture;
        private readonly Faker _faker;

        public ProductEntityTests(ProductFixture productFixture)
        {
            _faker = new Faker();
            _productFixture = productFixture;
        }

        [Fact]
        public void Product_IsValid_MustNotHaveErrors()
        {
            //Arrange            
            var entity = _productFixture.GenerateValidProduct();

            //Act
            bool valid = entity.IsValid();

            //Assert
            Assert.True(valid);
            Assert.False(entity.ValidationResult.Errors.Any());
        }


        [Fact]
        public void Product_InvalidName_Empty_ShouldNotBeValid()
        {
            // Arrange
            var product = _productFixture.GenerateValidProduct();
            product.SetName(string.Empty);

            // Act
            bool isValid = product.IsValid();

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void Product_InvalidName_TooLong_ShouldNotBeValid()
        {
            // Arrange
            var product = _productFixture.GenerateValidProduct();
            product.SetName(_faker.Commerce.ProductName().ClampLength(151));

            // Act
            bool isValid = product.IsValid();

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void Product_InvalidPrice_Negative_ShouldNotBeValid()
        {
            // Arrange
            var product = _productFixture.GenerateValidProduct();
            product.ChangePrice(-9.99m);

            // Act
            bool isValid = product.IsValid();

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void Product_InvalidDescription_TooLong_ShouldNotBeValid()
        {
            // Arrange
            var product = _productFixture.GenerateValidProduct();
            product.SetDescription(_faker.Commerce.ProductDescription().ClampLength(501));

            // Act
            bool isValid = product.IsValid();

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void Product_InvalidCategory_TooLong_ShouldNotBeValid()
        {
            // Arrange
            var product = _productFixture.GenerateValidProduct();
            product.ChangeCategory(_faker.Commerce.Categories(1).First().ClampLength(51));

            // Act
            bool isValid = product.IsValid();

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void Product_InvalidImageURL_TooLong_ShouldNotBeValid()
        {
            // Arrange
            var product = _productFixture.GenerateValidProduct();
            product.ChangeImageUrl("https://example.com/".ClampLength(301, 305));

            // Act
            bool isValid = product.IsValid();

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void Product_InvalidImageURL_InvalidFormat_ShouldNotBeValid()
        {
            // Arrange
            var product = _productFixture.GenerateValidProduct();
            product.ChangeImageUrl("not_a_valid_url");

            // Act
            bool isValid = product.IsValid();

            // Assert
            Assert.False(isValid);
        }
    }
}
