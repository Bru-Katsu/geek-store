using FluentValidation;
using GeekStore.Core.Models;

namespace GeekStore.Cart.Domain.Carts
{
    public class Cart : Entity
    {       
        private Cart(Guid userId, IEnumerable<CartItem> items)
        {
            _items = new Dictionary<Guid, CartItem>();
            Id = userId;
        }        
        
        public Guid? CouponId { get; private set; }
        public void SetCoupon(Guid couponId)
        {
            CouponId = couponId;
        }

        private IDictionary<Guid, CartItem> _items;
        public IReadOnlyCollection<CartItem> Items => _items.Values.ToList();
        public void AddItem(CartItem item)
        {
            if(!item.IsValid())
                throw new ArgumentException("Item inválido!", nameof(item));

            if (_items.ContainsKey(item.Id))
            {
                var refItem = _items[item.Id];

                refItem.SetName(item.Name);
                refItem.ChangePrice(item.Price);
                refItem.ChangeQuantity(item.Quantity);

                _items[item.Id] = refItem;
            }
            else
            {
                _items.Add(item.Id, item);
            }
        }

        public void RemoveItem(Guid productId)
        {
            if(_items.ContainsKey(productId))
                _items.Remove(productId);
        }

        public void Clear()
        {
            _items?.Clear();
        }

        public override bool IsValid()
        {
            ValidationResult = new CartEntityValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private class CartEntityValidation : AbstractValidator<Cart>
        {
            public CartEntityValidation()
            {
                RuleFor(x => x.CouponId)
                    .NotEqual(Guid.Empty)
                    .When(x => x.CouponId.HasValue)
                    .WithMessage("Id de cupom inválido!");

                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id de usuário inválido!");
            }
        }

        public static class Factory
        {
            public static Cart NewCart(Guid userId)
            {
                return new Cart(userId, new List<CartItem>());
            }

            public static Cart CreateWith(Guid userId, IEnumerable<CartItem> items)
            {
                return new Cart(userId, items);
            }
        }
    }
}
