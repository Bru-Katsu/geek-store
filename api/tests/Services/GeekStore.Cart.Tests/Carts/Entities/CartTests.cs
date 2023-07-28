using GeekStore.Cart.Domain.Carts;
using GeekStore.Cart.Tests.Fixtures;
using NuGet.Frameworks;

namespace GeekStore.Cart.Tests.Carts.Entities
{
    public class CartEntityTests : IClassFixture<CartItemFixture>
    {
        private readonly CartItemFixture _cartItemFixture;

        public CartEntityTests(CartItemFixture cartItemFixture)
        {
            _cartItemFixture = cartItemFixture;
        }

        #region Validation
        [Fact(DisplayName = "Ao criar um cart válido, método deve retornar válido")]
        [Trait("Entities", nameof(Domain.Carts.Cart))]
        public void Cart_IsValid_ShouldBeValid()
        {
            //arrange
            var entity = Domain.Carts.Cart.Factory.NewCart(Guid.NewGuid());
            entity.SetCoupon(Guid.NewGuid());

            //act
            var result = entity.IsValid();

            //assert
            Assert.True(result);
            Assert.False(entity.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Ao criar um cart com id de usuário inválido, método deve retornar inválido")]
        [Trait("Entities", nameof(Domain.Carts.Cart))]
        public void Cart_InvalidUserId_ShouldBeInvalid()
        {
            //arrange
            var entity = Domain.Carts.Cart.Factory.NewCart(Guid.Empty);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Carts.Cart.Id), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um cart com id de cupom inválido, método deve retornar inválido")]
        [Trait("Entities", nameof(Domain.Carts.Cart))]
        public void Cart_InvalidCouponId_ShouldBeInvalid()
        {
            //arrange
            var entity = Domain.Carts.Cart.Factory.NewCart(Guid.NewGuid());
            entity.SetCoupon(Guid.Empty);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Carts.Cart.CouponId), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }
        #endregion

        #region Methods
        [Fact(DisplayName = "Ao definir um cupom para o cart, método deve alterar a propriedade")]
        [Trait("Entities", nameof(Domain.Carts.Cart))]
        public void Cart_CoupounSet_ShouldChangeProperty()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var couponId = Guid.NewGuid();

            var cart = Domain.Carts.Cart.Factory.NewCart(userId);

            // Act
            cart.SetCoupon(couponId);

            // Assert
            Assert.Equal(couponId, cart.CouponId);
        }

        [Fact(DisplayName = "Ao remover um cupom do cart, método deve alterar a propriedade para nulo")]
        [Trait("Entities", nameof(Domain.Carts.Cart))]
        public void Cart_CouponUnset_ShouldChangePropertyToNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var couponId = Guid.NewGuid();

            var cart = Domain.Carts.Cart.Factory.CreateWith(userId, couponId, new List<CartItem>());
            cart.SetCoupon(couponId);

            // Act
            cart.UnsetCoupon();

            // Assert
            Assert.Null(cart.CouponId);
        }

        [Fact(DisplayName = "Ao adicionar um item válido, deve conter na listagem de itens")]
        [Trait("Entities", nameof(Domain.Carts.Cart))]
        public void Cart_AddValidItem_ShouldContainsItemInList()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cart = Domain.Carts.Cart.Factory.NewCart(userId);
            var item = _cartItemFixture.CreateValidItem();

            // Act
            cart.AddItem(item);

            // Assert
            Assert.Contains(item, cart.Items);            
        }

        [Fact(DisplayName = "Ao adicionar um item já existente, deve atualizar informações na listagem de itens")]
        [Trait("Entities", nameof(Domain.Carts.Cart))]
        public void Cart_ExistingItem_ShouldReplaceItemInfo()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var item = _cartItemFixture.CreateValidItem();
            var cart = Domain.Carts.Cart.Factory.CreateWith(userId, null, new List<CartItem> { item });

            var newItem = _cartItemFixture.CreateValidItemWithId(item.Id);

            // Act
            cart.AddItem(newItem);

            // Assert
            Assert.Contains(item, cart.Items);
            Assert.Single(cart.Items);
        }

        [Fact(DisplayName = "Ao adicionar um item inválido, deve retornar erros de validações dentro da classe cart")]
        [Trait("Entities", nameof(Domain.Carts.Cart))]
        public void Cart_AddInvalidItem_ShouldNotContainsInList()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cart = Domain.Carts.Cart.Factory.NewCart(userId);
            var item = _cartItemFixture.CreateInvalidItem();

            // Act
            cart.AddItem(item);

            // Assert
            Assert.Empty(cart.Items);
            Assert.NotEmpty(cart.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Ao remover um item, não deve constar na listagem de itens")]
        [Trait("Entities", nameof(Domain.Carts.Cart))]
        public void Cart_RemoveItem_ShouldBeRemovedFromList()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var items = _cartItemFixture.CreateValidItems().ToList();

            var pos = new Random().Next(0, 9);
            var removeItem = items.Skip(pos).First();

            var cart = Domain.Carts.Cart.Factory.CreateWith(userId, null, items);

            //Act
            cart.RemoveItem(removeItem.Id);

            //Assert
            Assert.DoesNotContain(removeItem, cart.Items);
        }
        #endregion
    }
}
