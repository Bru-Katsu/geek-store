using Bogus;
using GeekStore.Cart.Tests.Common;

namespace GeekStore.Cart.Tests.Fixtures
{
    public class CartItemFixture : ICollectionFixture<CartItemFixture>
    {
        private readonly Faker _faker;

        public CartItemFixture()
        {
            _faker = new(Constants.LOCALE);
        }

        public Domain.Carts.CartItem CreateValidItem()
        {
            return new Domain.Carts.CartItem(Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Random.Int(min: 1), _faker.Random.Decimal(min: 1));
        }
    }
}
