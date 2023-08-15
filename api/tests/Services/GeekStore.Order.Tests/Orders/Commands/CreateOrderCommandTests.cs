using Bogus;
using GeekStore.Order.Application.Orders.Commands;
using GeekStore.Order.Application.Orders.DTOs;

namespace GeekStore.Order.Tests.Orders.Commands
{
    public class CreateOrderCommandTests
    {
        private readonly Faker _faker;
        public CreateOrderCommandTests()
        {
            _faker = new();
        }

        [Fact(DisplayName = "Comando de criar pedido deve ser válido")]
        [Trait("Commands", nameof(CreateOrderCommand))]
        public void CreateOrderCommand_ShouldBeValid_WhenValidInput()
        {
            //Arrange
            var address = new AddressDTO(_faker.Address.City(), _faker.Address.Country(), _faker.Address.StateAbbr(), _faker.Address.StreetName(), _faker.Address.ZipCode());
            var coupon = new CouponDTO("GEEK20", 20);
            var items = _faker.Make(10, () => new OrderItemDTO(Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Image.Image(), _faker.Random.Int(min: 1), _faker.Random.Decimal(min: 1)));

            var command = new CreateOrderCommand(Guid.NewGuid(), address, coupon, items);
            //Act
            var isValid = command.IsValid();

            //Assert
            Assert.True(isValid);
            Assert.Empty(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o id do usuario é inválido")]
        [Trait("Commands", nameof(CreateOrderCommand))]
        public void CreateOrderCommand_ShouldHaveValidationError_WhenUserIdInvalid()
        {
            //Arrange
            var address = new AddressDTO(_faker.Address.City(), _faker.Address.Country(), _faker.Address.StateAbbr(), _faker.Address.StreetName(), _faker.Address.ZipCode());
            var coupon = new CouponDTO("GEEK20", 20);
            var items = _faker.Make(10, () => new OrderItemDTO(Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Image.Image(), _faker.Random.Int(min: 1), _faker.Random.Decimal(min: 1)));

            var command = new CreateOrderCommand(Guid.Empty, address, coupon, items);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Single(command.ValidationResult.Errors);
            Assert.Contains(nameof(CreateOrderCommand.UserId), command.ValidationResult.Errors.Select(x => x.PropertyName));
        }


        [Fact(DisplayName = "Deve ter erro de validação quando o endereço é inválido")]
        [Trait("Commands", nameof(CreateOrderCommand))]
        public void CreateOrderCommand_ShouldHaveValidationError_WhenAddressInvalid()
        {
            //Arrange
            var address = new AddressDTO(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            var coupon = new CouponDTO("GEEK20", 20);
            var items = _faker.Make(10, () => new OrderItemDTO(Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Image.Image(), _faker.Random.Int(min: 1), _faker.Random.Decimal(min: 1)));

            var command = new CreateOrderCommand(Guid.NewGuid(), address, coupon, items);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Contains("Address.Country", command.ValidationResult.Errors.Select(x => x.PropertyName));
            Assert.Contains("Address.State", command.ValidationResult.Errors.Select(x => x.PropertyName));
            Assert.Contains("Address.Street", command.ValidationResult.Errors.Select(x => x.PropertyName));
            Assert.Contains("Address.City", command.ValidationResult.Errors.Select(x => x.PropertyName));
            Assert.Contains("Address.ZipCode", command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o cupom é inválido")]
        [Trait("Commands", nameof(CreateOrderCommand))]
        public void CreateOrderCommand_ShouldHaveValidationError_WhenCouponInvalid()
        {
            //Arrange
            var address = new AddressDTO(_faker.Address.City(), _faker.Address.Country(), _faker.Address.StateAbbr(), _faker.Address.StreetName(), _faker.Address.ZipCode());
            var coupon = new CouponDTO(string.Empty, -1);
            var items = _faker.Make(10, () => new OrderItemDTO(Guid.NewGuid(), _faker.Commerce.ProductName(), _faker.Image.Image(), _faker.Random.Int(min: 1), _faker.Random.Decimal(min: 1)));

            var command = new CreateOrderCommand(Guid.NewGuid(), address, coupon, items);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Contains("Coupon.CouponCode", command.ValidationResult.Errors.Select(x => x.PropertyName));
            Assert.Contains("Coupon.DiscountAmount", command.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Deve ter erro de validação quando o item é inválido")]
        [Trait("Commands", nameof(CreateOrderCommand))]
        public void CreateOrderCommand_ShouldHaveValidationError_WhenItemsInvalid()
        {
            //Arrange
            var address = new AddressDTO(_faker.Address.City(), _faker.Address.Country(), _faker.Address.StateAbbr(), _faker.Address.StreetName(), _faker.Address.ZipCode());
            var coupon = new CouponDTO("GEEK20", 20);
            var items = _faker.Make(1, () => new OrderItemDTO(Guid.Empty, string.Empty, string.Empty, -1, -1));

            var command = new CreateOrderCommand(Guid.NewGuid(), address, coupon, items);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Contains("Items[0].ProductId", command.ValidationResult.Errors.Select(x => x.PropertyName));
            Assert.Contains("Items[0].ProductName", command.ValidationResult.Errors.Select(x => x.PropertyName));
            Assert.Contains("Items[0].ProductImage", command.ValidationResult.Errors.Select(x => x.PropertyName));
            Assert.Contains("Items[0].Quantity", command.ValidationResult.Errors.Select(x => x.PropertyName));
            Assert.Contains("Items[0].Price", command.ValidationResult.Errors.Select(x => x.PropertyName));
        }
    }
}
