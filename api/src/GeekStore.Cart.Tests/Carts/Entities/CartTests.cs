namespace GeekStore.Cart.Tests.Carts.Entities
{
    public class CartEntityTests
    {
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

        #region Setters
        [Fact(DisplayName = "Ao definir um cupom para o cart, método deve alterar a propriedade")]
        [Trait("Entities", nameof(Domain.Carts.Cart))]
        public void Cart_CoupounSet_ShouldChangeProperty()
        {
            // Arrange
            var couponId = Guid.NewGuid();
            var cart = Domain.Carts.Cart.Factory.NewCart(couponId);

            // Act
            cart.SetCoupon(couponId);

            // Assert
            Assert.Equal(couponId, cart.CouponId);
        }
        #endregion
    }
}
