using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekStore.Customer.Tests.Fixtures
{
    public class CustomerFixture : ICollectionFixture<CustomerFixture>
    {
        private readonly Faker _faker;

        public CustomerFixture()
        {
            _faker = new();
        }

        public IEnumerable<Domain.Customers.Customer> CreateValidCustomers(int quantity = 10)
        {
            return _faker.Make(quantity, CreateValidCustomer);
        }

        public Domain.Customers.Customer CreateValidCustomer()
        {
            return new Domain.Customers.Customer(Guid.NewGuid(), _faker.Name.FirstName(), _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email);
        }

        public Domain.Customers.Customer CreateInvalidCustomer()
        {
            return new Domain.Customers.Customer(Guid.NewGuid(), string.Empty, string.Empty, DateTime.Now.AddDays(1), string.Empty, string.Empty);
        }
    }
}
