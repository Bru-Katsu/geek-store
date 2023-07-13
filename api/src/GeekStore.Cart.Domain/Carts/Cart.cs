using FluentValidation;
using GeekStore.Core.Models;

namespace GeekStore.Cart.Domain.Carts
{
    public class Cart : Entity
    {       
        private Cart(Guid userId, IEnumerable<CartItem> items)
        {
            _items = new Dictionary<Guid, CartItem>();
            UserId = userId;
        }

        private IDictionary<Guid, CartItem> _items;
        public IReadOnlyCollection<CartItem> Items => _items.Values.ToList();

        public Guid UserId { get; private set; }

        public void AddItem(CartItem item)
        {
            if(!item.IsValid())
                throw new ArgumentException("Item inválido!", nameof(item));

            if (_items.ContainsKey(item.ProductId))
            {
                var refItem = _items[item.ProductId];

                refItem.SetName(item.ProductName);
                refItem.ChangePrice(item.Price);
                refItem.ChangeQuantity(item.Quantity + refItem.Quantity);

                _items[item.ProductId] = refItem;
            }
            else
            {
                _items.Add(item.ProductId, item);
            }
        }

        public void RemoveItem(CartItem item)
        {
            if(_items.ContainsKey(item.ProductId))
                _items.Remove(item.ProductId);
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
                RuleFor(x => x.UserId)
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
