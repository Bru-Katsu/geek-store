using Bogus;
using Bogus.Extensions;
using Bogus.Extensions.Brazil;

namespace GeekStore.Customer.Tests.Customers.Entities
{
    public class CustomerEntityTests
    {
        private Faker _faker;

        public CustomerEntityTests()
        {
            _faker = new();
        }

        [Fact(DisplayName = "Ao criar um cliente válido, método deve retornar válido")]
        [Trait("Entities", "Customer")]
        public void Customer_ValidData_ShouldBeValid()
        {
            //arrange
            var entity = new Domain.Customers.Customer(Guid.NewGuid(), _faker.Name.FirstName(), _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email);

            //act
            var result = entity.IsValid();

            //assert
            Assert.True(result);
            Assert.False(entity.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Ao criar um cliente com userid vazio, método deve retornar inválido")]
        [Trait("Entities", "Customer")]
        public void Customer_EmptyUserId_ShouldBeInvalid()
        {
            //arrange
            var entity = new Domain.Customers.Customer(Guid.Empty, _faker.Name.FirstName(), _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Customers.Customer.UserId), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um cliente com nome vazio, método deve retornar inválido")]
        [Trait("Entities", "Customer")]
        public void Customer_NameEmpty_ShouldBeInvalid()
        {
            //arrange
            var entity = new Domain.Customers.Customer(Guid.NewGuid(), string.Empty, _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Customers.Customer.Name), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um cliente com nome maior que o permitido, método deve retornar inválido")]
        [Trait("Entities", "Customer")]
        public void Customer_NameGreaterThanAllowed_ShouldBeInvalid()
        {
            //arrange
            var entity = new Domain.Customers.Customer(Guid.NewGuid(), _faker.Name.FirstName().ClampLength(min: 256), _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Customers.Customer.Name), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um cliente com sobrenome vazio, método deve retornar inválido")]
        [Trait("Entities", "Customer")]
        public void Customer_SurnameNameEmpty_ShouldBeInvalid()
        {
            //arrange
            var entity = new Domain.Customers.Customer(Guid.NewGuid(), _faker.Name.FirstName(), string.Empty, _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Customers.Customer.Surname), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um cliente com sobrenome maior que o permitido, método deve retornar inválido")]
        [Trait("Entities", "Customer")]
        public void Customer_SurnameGreaterThanAllowed_ShouldBeInvalid()
        {
            //arrange
            var entity = new Domain.Customers.Customer(Guid.NewGuid(), _faker.Name.FirstName(), _faker.Name.LastName().ClampLength(min: 256), _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Customers.Customer.Surname), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um cliente com data de nascimento no futuro, método deve retornar inválido")]
        [Trait("Entities", "Customer")]
        public void Customer_BirthdayOnFuture_ShouldBeInvalid()
        {
            //arrange
            var entity = new Domain.Customers.Customer(Guid.NewGuid(), _faker.Name.FirstName(), _faker.Name.LastName(), DateTime.Now.AddDays(1), _faker.Person.Cpf(), _faker.Person.Email);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Customers.Customer.Birthday), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um cliente com documento vazio, método deve retornar inválido")]
        [Trait("Entities", "Customer")]
        public void Customer_DocumentEmpty_ShouldBeInvalid()
        {
            //arrange
            var entity = new Domain.Customers.Customer(Guid.NewGuid(), _faker.Name.FirstName(), _faker.Name.LastName(), _faker.Date.Past(), string.Empty, _faker.Person.Email);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Customers.Customer.Document), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um cliente com documento maior que o permitido, método deve retornar inválido")]
        [Trait("Entities", "Customer")]
        public void Customer_DocumentGreaterThanAllowed_ShouldBeInvalid()
        {
            //arrange
            var entity = new Domain.Customers.Customer(Guid.NewGuid(), _faker.Name.FirstName(), _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf().ClampLength(min: 101), _faker.Person.Email);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Customers.Customer.Document), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um cliente com email vazio, método deve retornar inválido")]
        [Trait("Entities", "Customer")]
        public void Customer_EmailEmpty_ShouldBeInvalid()
        {
            //arrange
            var entity = new Domain.Customers.Customer(Guid.NewGuid(), _faker.Name.FirstName(), _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf(), string.Empty);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Contains(nameof(Domain.Customers.Customer.Email), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um cliente com email maior que o permitido, método deve retornar inválido")]
        [Trait("Entities", "Customer")]
        public void Customer_EmailGreaterThanAllowed_ShouldBeInvalid()
        {
            //arrange
            var entity = new Domain.Customers.Customer(Guid.NewGuid(), _faker.Name.FirstName(), _faker.Name.LastName(), _faker.Date.Past(), _faker.Person.Cpf(), _faker.Person.Email.ClampLength(min: 513));

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Single(entity.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Customers.Customer.Email), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }
    }
}
