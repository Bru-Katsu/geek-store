using Bogus;
using GeekStore.Order.Domain.Orders;

namespace GeekStore.Order.Tests.Fixtures
{
    public class OrderItemFixture : ICollectionFixture<OrderItemFixture>
    {
        private readonly Faker _faker;
        public OrderItemFixture()
        {
            _faker = new Faker();
        }

        public IEnumerable<OrderItem> CreateItemsWithNegativePrices()
        {
            return _faker.Make(10, CreateItemWithNegativePrice);
        }

        public OrderItem CreateItemWithNegativePrice()
        {
            return new OrderItem(Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Image.Image(), _faker.Random.Int(min: 1), _faker.Random.Decimal(max: -1));
        }

        public IEnumerable<OrderItem> CreateValidItems()
        {
            return _faker.Make(10, CreateValidItem);
        }

        public OrderItem CreateValidItem()
        {
            return new OrderItem(Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Image.Image(), _faker.Random.Int(min: 1), _faker.Random.Decimal(min:1));
        }
    }
}
