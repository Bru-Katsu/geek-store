using GeekStore.Order.Domain.Orders;
using GeekStore.Order.Domain.Orders.Enums;
using GeekStore.Order.Domain.Orders.ValueObjects;
using GeekStore.Order.Tests.Fixtures;


namespace GeekStore.Order.Tests.Orders.Entities
{
    public class OrderEntityTests : IClassFixture<OrderItemFixture>,
                                    IClassFixture<AddressFixture>,
                                    IClassFixture<OrderFixture>
    {
        private readonly OrderItemFixture _orderItemFixture;
        private readonly AddressFixture _addressFixture;
        private readonly OrderFixture _orderFixture;
        public OrderEntityTests(OrderItemFixture fixture,
                                AddressFixture addressFixture,
                                OrderFixture orderFixture)
        {
            _orderItemFixture = fixture;
            _addressFixture = addressFixture;
            _orderFixture = orderFixture;
        }

        [Fact(DisplayName = "Ao criar um pedido válido, método deve retornar válido")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_IsValid_ShouldBeValid()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var validItems = _orderItemFixture.CreateValidItems();

            var order = new Domain.Orders.Order(userId, validItems);
            var address = _addressFixture.CreateValidAddress();

            order.DefineAddress(address);

            // Act
            var result = order.IsValid();

            // Assert
            Assert.True(result);
            Assert.Empty(order.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Ao criar um pedido sem itens, método deve retornar inválido")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_EmptyItems_ShouldBeInvalid()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var order = new Domain.Orders.Order(userId, Enumerable.Empty<OrderItem>());
            var address = _addressFixture.CreateValidAddress();

            order.DefineAddress(address);

            // Act
            var result = order.IsValid();

            // Assert
            Assert.False(result);
            Assert.NotEmpty(order.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Orders.Order.OrderItems), order.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao aplicar um cupom válido, o valor total do pedido deve ser atualizado corretamente")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_ApplyValidCoupon_ShouldUpdateTotalCorrectly()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var validItems = _orderItemFixture.CreateValidItems();
            var address = _addressFixture.CreateValidAddress();
            
            var order = new Domain.Orders.Order(userId, validItems);
            order.DefineAddress(address);

            var coupon = new Coupon("SUMMER20", 20); // 20% discount

            // Act
            order.ApplyCoupon(coupon);
            order.CalculateAmount();

            // Assert
            decimal itemsTotal = validItems.Sum(item => item.CalculateValue());
            decimal expectedTotalWithDiscount =  itemsTotal * 0.8m; // 20% discount

            Assert.Equal(expectedTotalWithDiscount, order.Total);
            Assert.Equal(itemsTotal * 0.2m, order.TotalDiscount);
        }

        [Fact(DisplayName = "Ao aplicar um cupom inválido, pedido deve constar como inválido")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_ApplyInvalidCoupon_ShouldThrowException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var validItems = _orderItemFixture.CreateValidItems();
            var address = _addressFixture.CreateValidAddress();

            var order = new Domain.Orders.Order(userId, validItems);
            order.DefineAddress(address);

            var coupon = new Coupon(string.Empty, -1); // Invalid coupon

            // Act
            order.ApplyCoupon(coupon);

            // Assert
            Assert.False(order.IsValid());
        }


        [Fact(DisplayName = "Ao tentar definir um endereço inválido, pedido deve estar inválido")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_SetInvalidAddress_ShouldThrowException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var validItems = _orderItemFixture.CreateValidItems();
            var address = _addressFixture.CreateInvalidAddress();

            var order = new Domain.Orders.Order(userId, validItems);

            // Act
            order.DefineAddress(address);

            // Assert
            Assert.False(order.IsValid());
        }

        [Fact(DisplayName = "Ao calcular o valor total do pedido sem cupom de desconto, o valor total deve ser igual ao somatório dos itens")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_CalculateTotalWithoutCoupon_ShouldMatchItemsTotal()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var validItems = _orderItemFixture.CreateValidItems();
            var address = _addressFixture.CreateValidAddress();

            var order = new Domain.Orders.Order(userId, validItems);
            order.DefineAddress(address);

            // Act
            order.CalculateAmount();

            // Assert
            decimal expectedTotal = validItems.Sum(item => item.CalculateValue());
            Assert.Equal(expectedTotal, order.Total);
            Assert.Equal(0, order.TotalDiscount);
        }

        [Fact(DisplayName = "Ao calcular o valor total do pedido com cupom de desconto inválido, o valor total deve ser igual ao somatório dos itens")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_CalculateTotalWithInvalidCoupon_ShouldMatchItemsTotal()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var validItems = _orderItemFixture.CreateValidItems();
            var address = _addressFixture.CreateInvalidAddress();

            var order = new Domain.Orders.Order(userId, validItems);
            order.DefineAddress(address);
            order.ApplyCoupon(new Coupon("INVALID", -1));

            // Act
            order.CalculateAmount();

            // Assert
            decimal expectedTotal = validItems.Sum(item => item.CalculateValue());
            Assert.Equal(expectedTotal, order.Total);
            Assert.Equal(0, order.TotalDiscount);
        }

        [Fact(DisplayName = "Ao autorizar o pedido, deve alterar o status")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_AuthorizeOrder_ShouldChangeStatus()
        {
            // Arrange
            var items = _orderItemFixture.CreateValidItems();
            var order = _orderFixture.CreateValidOrder(items);

            // Act
            order.Authorize();

            // Assert
            Assert.Equal(OrderStatus.Authorized, order.Status);
        }

        [Fact(DisplayName = "Ao cancelar o pedido, deve alterar o status")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_CancelOrder_ShouldChangeStatus()
        {
            // Arrange
            var items = _orderItemFixture.CreateValidItems();
            var order = _orderFixture.CreateValidOrder(items);

            // Act
            order.Cancel();

            // Assert
            Assert.Equal(OrderStatus.Canceled, order.Status);
        }

        [Fact(DisplayName = "Ao pagar o pedido, deve alterar o status")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_PayOrder_ShouldChangeStatus()
        {
            // Arrange
            var items = _orderItemFixture.CreateValidItems();
            var order = _orderFixture.CreateValidOrder(items);

            // Act
            order.Pay();

            // Assert
            Assert.Equal(OrderStatus.Paid, order.Status);
        }

        [Fact(DisplayName = "Ao negar o pedido, deve alterar o status")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_RefuseOrder_ShouldChangeStatus()
        {
            // Arrange
            var items = _orderItemFixture.CreateValidItems();
            var order = _orderFixture.CreateValidOrder(items);

            // Act
            order.Refuse();

            // Assert
            Assert.Equal(OrderStatus.Refused, order.Status);
        }

        [Fact(DisplayName = "Ao entregar o pedido, deve alterar o status")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_DeliveryOrder_ShouldChangeStatus()
        {
            // Arrange
            var items = _orderItemFixture.CreateValidItems();
            var order = _orderFixture.CreateValidOrder(items);

            // Act
            order.Delivery();

            // Assert
            Assert.Equal(OrderStatus.Delivered, order.Status);
        }

        [Fact(DisplayName = "Ao criar um pedido, status deve estar como pendente")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_NewOrder_ShouldHasStatusPending()
        {
            // Arrange
            var items = _orderItemFixture.CreateValidItems();

            // Act
            var order = _orderFixture.CreateValidOrder(items);

            // Assert
            Assert.Equal(OrderStatus.Pending, order.Status);
        }

        [Fact(DisplayName = "Endereço não definido deve retornar inválido")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_AddressNotDefined_ShouldBeInvalid()
        {
            // Arrange
            var userId = Guid.NewGuid();
            
            var items = _orderItemFixture.CreateValidItems();
            var order = new Domain.Orders.Order(userId, items);
            
            order.CalculateAmount();

            // Act
            order.DefineAddress(null);

            // Assert
            Assert.False(order.IsValid());
            Assert.Single(order.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Orders.Order.Address), order.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Total negativo deve retornar inválido")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_TotalNegative_ShouldBeInvalid()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var items = _orderItemFixture.CreateItemsWithNegativePrices();
            var order = new Domain.Orders.Order(userId, items);
            var address = _addressFixture.CreateValidAddress();
            order.DefineAddress(address);

            // Act
            order.CalculateAmount();

            // Assert
            Assert.False(order.IsValid());
            Assert.Single(order.ValidationResult.Errors);
            Assert.Contains(nameof(Domain.Orders.Order.Total), order.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Total de desconto do cupom negativo deve retornar inválido")]
        [Trait("Entities", nameof(Domain.Orders.Order))]
        public void Order_TotalDiscountNegative_ShouldBeInvalid()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var items = _orderItemFixture.CreateItemsWithNegativePrices();
            var order = new Domain.Orders.Order(userId, items);
            var address = _addressFixture.CreateValidAddress();

            order.DefineAddress(address);
            order.ApplyCoupon(new Coupon("GEEK20", 20));

            // Act
            order.CalculateAmount();

            // Assert
            Assert.False(order.IsValid());
            Assert.Contains(nameof(Domain.Orders.Order.Total), order.ValidationResult.Errors.Select(x => x.PropertyName));
            Assert.Contains(nameof(Domain.Orders.Order.TotalDiscount), order.ValidationResult.Errors.Select(x => x.PropertyName));
        }
    }
}
