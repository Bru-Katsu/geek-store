using GeekStore.Customer.Domain.Customers;
using GeekStore.Customer.Domain.Customers.Repositories;
using GeekStore.Customer.Domain.Customers.Specifications;
using GeekStore.Customer.Tests.Fixtures;
using Moq;
using Moq.AutoMock;
using System.Linq.Expressions;

namespace GeekStore.Customer.Tests.Customers.Specifications
{
    public class NotAllowDuplicateCustomerDocumentsSpecTests : IClassFixture<CustomerFixture>
    {
        private readonly CustomerFixture _customerFixture;

        public NotAllowDuplicateCustomerDocumentsSpecTests(CustomerFixture customerFixture)
        {
            _customerFixture = customerFixture;
        }

        [Fact(DisplayName = "Cliente com documento não registrado deve satisfazer condição")]
        [Trait("Specifications", nameof(NotAllowDuplicateCustomerDocumentsSpec))]
        public async Task NotAllowDuplicateCustomerDocumentsSpec_CustomerWithUniqueDocument_ShouldBeSatisfied()
        {
            //Arrange
            var mocker = new AutoMocker();
            var spec = mocker.CreateInstance<NotAllowDuplicateCustomerDocumentsSpec>();
            var customer = _customerFixture.CreateValidCustomer();

            mocker
                .GetMock<ICustomerRepository>()
                .Setup(repo => repo.Filter(It.IsAny<Expression<Func<Domain.Customers.Customer, bool>>>()))
                .ReturnsAsync(Array.Empty<Domain.Customers.Customer>);

            //Act
            var result = await spec.IsSatisfiedBy(customer);

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Cliente com documento já registrado não deve satisfazer condição")]
        [Trait("Specifications", nameof(NotAllowDuplicateCustomerDocumentsSpec))]
        public async Task NotAllowDuplicateCustomerDocumentsSpec_CustomerWithSameDocument_ShouldBeNotSatisfied()
        {
            //Arrange
            var mocker = new AutoMocker();
            var spec = mocker.CreateInstance<NotAllowDuplicateCustomerDocumentsSpec>();
            var customer = _customerFixture.CreateValidCustomer();

            mocker
                .GetMock<ICustomerRepository>()
                .Setup(repo => repo.Filter(It.IsAny<Expression<Func<Domain.Customers.Customer, bool>>>()))
                .ReturnsAsync(new[] 
                {
                    customer
                });

            //Act
            var result = await spec.IsSatisfiedBy(customer);

            //Assert
            Assert.False(result);
        }
    }
}
