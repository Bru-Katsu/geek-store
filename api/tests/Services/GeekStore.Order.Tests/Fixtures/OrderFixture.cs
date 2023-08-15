using Bogus;
using GeekStore.Order.Domain.Orders;
using GeekStore.Order.Domain.Orders.ValueObjects;

namespace GeekStore.Order.Tests.Fixtures
{
    public class OrderFixture : ICollectionFixture<OrderFixture>
    {
        private readonly Faker _faker;
        public OrderFixture()
        {
            _faker = new();
        }

        public Domain.Orders.Order CreateValidOrder(IEnumerable<OrderItem> items)
        {
            var userId = Guid.NewGuid();
            var order = new Domain.Orders.Order(userId, items);
            var address = new Address(_faker.Address.StreetAddress(), _faker.Address.City(), _faker.Address.StateAbbr(), _faker.Address.County(), _faker.Address.ZipCode());

            order.DefineAddress(address);
            order.CalculateAmount();

            return order;
        }
    }
}
