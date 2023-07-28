using Bogus;
using GeekStore.Cart.Tests.Fixtures;

namespace GeekStore.Cart.Tests.Carts.Entities
{
    public class CartItemTests : IClassFixture<CartItemFixture>
    {
        private readonly Faker _faker;
        private readonly CartItemFixture _cartItemFixture;
        public CartItemTests(CartItemFixture cartItemFixture)
        {
            _faker = new();
            _cartItemFixture = cartItemFixture;
        }

        #region Validation
        [Fact(DisplayName = "Ao criar um item do cart válido, método deve retornar válido")]
        [Trait("Entities", nameof(Domain.Carts.CartItem))]
        public void CartItem_IsValid_ShouldBeValid()
        {
            //arrange
            var entity = _cartItemFixture.CreateValidItem();

            //act
            var result = entity.IsValid();

            //assert
            Assert.True(result);
            Assert.False(entity.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Ao criar um item do cart com nome do produto em branco, método deve retornar inválido")]
        [Trait("Entities", nameof(Domain.Carts.CartItem))]
        public void CartItem_EmptyName_ShouldBeInvalid()
        {
            //arrange
            var entity = _cartItemFixture.CreateValidItem();
            entity.SetName(string.Empty);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Carts.CartItem.Name), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um item do cart com quantidade igual a zero, método deve retornar inválido")]
        [Trait("Entities", nameof(Domain.Carts.CartItem))]
        public void CartItem_QuantityEqualZero_ShouldBeInvalid()
        {
            //arrange
            var entity = _cartItemFixture.CreateValidItem();
            entity.ChangeQuantity(0);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Carts.CartItem.Quantity), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um item do cart com quantidade menor do que zero, método deve retornar inválido")]
        [Trait("Entities", nameof(Domain.Carts.CartItem))]
        public void CartItem_QuantityLessThanZero_ShouldBeInvalid()
        {
            //arrange
            var entity = _cartItemFixture.CreateValidItem();
            entity.ChangeQuantity(-10);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Carts.CartItem.Quantity), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um item do cart com preço igual a zero, método deve retornar inválido")]
        [Trait("Entities", nameof(Domain.Carts.CartItem))]
        public void CartItem_PriceEqualZero_ShouldBeInvalid()
        {
            //arrange
            var entity = _cartItemFixture.CreateValidItem();
            entity.ChangePrice(0);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Carts.CartItem.Price), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um item do cart com preço menor que zero, método deve retornar inválido")]
        [Trait("Entities", nameof(Domain.Carts.CartItem))]
        public void CartItem_PriceLessThanZero_ShouldBeInvalid()
        {
            //arrange
            var entity = _cartItemFixture.CreateValidItem();
            entity.ChangePrice(-50);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Carts.CartItem.Price), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }
        #endregion

        #region Setters
        [Fact(DisplayName = "Ao definir um novo nome para o item do cart, método deve alterar a propriedade")]
        [Trait("Entities", nameof(Domain.Carts.CartItem))]
        public void CartItem_ProductNameSet_ShouldChangeProperty()
        {
            // Arrange
            var newName = _faker.Commerce.ProductName();
            var entity = _cartItemFixture.CreateValidItem();

            // Act
            entity.SetName(newName);

            // Assert
            Assert.Equal(newName, entity.Name);
        }

        [Fact(DisplayName = "Ao definir um novo preço para o item do cart, método deve alterar a propriedade")]
        [Trait("Entities", nameof(Domain.Carts.CartItem))]
        public void CartItem_ProductPriceSet_ShouldChangeProperty()
        {
            // Arrange
            var newPrice = _faker.Random.Decimal(min: 1);
            var entity = _cartItemFixture.CreateValidItem();

            // Act
            entity.ChangePrice(newPrice);

            // Assert
            Assert.Equal(newPrice, entity.Price);
        }

        [Fact(DisplayName = "Ao definir uma nova quantidade para o item do cart, método deve alterar a propriedade")]
        [Trait("Entities", nameof(Domain.Carts.CartItem))]
        public void CartItem_ProductQuantitySet_ShouldChangeProperty()
        {
            // Arrange
            var newQuantity = _faker.Random.Int(min: 1);
            var entity = _cartItemFixture.CreateValidItem();

            // Act
            entity.ChangeQuantity(newQuantity);

            // Assert
            Assert.Equal(newQuantity, entity.Quantity);
        }
        #endregion
    }
}
