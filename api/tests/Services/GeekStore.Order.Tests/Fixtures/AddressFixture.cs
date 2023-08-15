using Bogus;
using GeekStore.Order.Domain.Orders.ValueObjects;

namespace GeekStore.Order.Tests.Fixtures
{
    public class AddressFixture : ICollectionFixture<AddressFixture>
    {
        private readonly Faker _faker;

        public AddressFixture()
        {
            _faker = new();
        }

        public Address CreateValidAddress()
        {
            return new Address(_faker.Address.StreetAddress(),_faker.Address.City(), _faker.Address.StateAbbr(), _faker.Address.County(), _faker.Address.ZipCode());
        }

        public Address CreateInvalidAddress()
        {
            return new Address(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }
    }
}
