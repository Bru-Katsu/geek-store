using FluentValidation;
using FluentValidation.Results;
using GeekStore.Core.Extensions;
using GeekStore.Core.Models;
using GeekStore.Order.Domain.Orders.Enums;
using GeekStore.Order.Domain.Orders.ValueObjects;

namespace GeekStore.Order.Domain.Orders
{
    public class Order : Entity
    {
        protected Order() { }
        public Order(Guid userId, IEnumerable<OrderItem> items)
        {
            Id = Guid.NewGuid();

            UserId = userId;
            CreationDate = DateTime.Now;
            Status = OrderStatus.Pending;

            AddItems(items);
        }

        public Guid UserId { get; private set; }
        public DateTime CreationDate { get; private set; }        

        public Address Address { get; private set; }
        public void DefineAddress(Address address)
        {
            Address = address;
        }

        public Coupon? Coupon { get; private set; }
        public void ApplyCoupon(Coupon coupon)
        {
            Coupon = coupon;
        }

        private readonly List<OrderItem> _ordersItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems => _ordersItems;

        public void AddItem(OrderItem item)
        {
            _ordersItems.Add(item);
        }

        public void AddItems(IEnumerable<OrderItem> items)
        {
            foreach(var item in items)
                AddItem(item);
        }

        public OrderStatus Status { get; private set; }

        public void Authorize()
        {
            Status = OrderStatus.Authorized;
        }

        public void Pay()
        {
            Status = OrderStatus.Paid;
        }

        public void Refuse()
        {
            Status = OrderStatus.Refused;
        }

        public void Delivery()
        {
            Status = OrderStatus.Delivered;
        }

        public void Cancel()
        {
            Status = OrderStatus.Canceled;
        }

        public decimal Total { get; private set; }
        public void CalculateAmount()
        {
            Total = _ordersItems.Sum(x => x.CalculateValue());
            CalculateDiscount();
        }
        
        public decimal TotalDiscount { get; private set; }
        public void CalculateDiscount()
        {
            if (Coupon == null)
                return;

            if (!Coupon.Validate().IsValid)
                return;

            decimal value = Total;
            decimal discount = value * Coupon.DiscountAmount.Value / 100;

            value -= discount;

            Total = value;
            TotalDiscount = discount;
        }

        public override bool IsValid()
        {
            var orderValidationResult = new OrderEntityValidation().Validate(this);
            ValidationResult? addressValidationResult = Address?.Validate();
            ValidationResult? couponValidationResult = Coupon?.Validate();

            ValidationResult result = orderValidationResult;

            if(addressValidationResult != null)
                result = result.Merge(addressValidationResult);

            if(couponValidationResult != null)
                result = result.Merge(couponValidationResult);

            ValidationResult = result;

            return ValidationResult.IsValid;
        }

        private class OrderEntityValidation : AbstractValidator<Order>
        {
            public OrderEntityValidation()
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id inválido!");

                RuleFor(X => X.CreationDate)
                    .NotEmpty()
                    .WithMessage("Data de criação inválida!");

                RuleFor(x => x.Address)
                    .NotNull()
                    .WithMessage("Endereço não definido!");

                RuleFor(x => x.OrderItems)
                    .NotEmpty()
                    .WithMessage("Não há itens para o pedido!");

                RuleFor(x => x.Total)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Não é permitido valores negativos para o total!");

                RuleFor(x => x.TotalDiscount)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Não é permitido valores negativos para o total de desconto!");
            }
        }
    }
}
