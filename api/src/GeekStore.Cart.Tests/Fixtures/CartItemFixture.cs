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

        public Domain.Carts.CartItem CreateInvalidItem()
        {
            return new Domain.Carts.CartItem(Guid.Empty, string.Empty, -5, -5);
        }

        public Domain.Carts.CartItem CreateValidItem()
        {
            return CreateValidItemWithId(Guid.NewGuid());
        }

        public IEnumerable<Domain.Carts.CartItem> CreateValidItems()
        {
            for(var i = 0; i < 10; i++)
                yield return CreateValidItem();
        }

        public Domain.Carts.CartItem CreateValidItemWithId(Guid id)
        {
            return new Domain.Carts.CartItem(id, _faker.Commerce.ProductName(), _faker.Random.Int(min: 1), _faker.Random.Decimal(min: 1));
        }
    }
}
