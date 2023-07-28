using GeekStore.Cart.Data.DTOs;
using GeekStore.Cart.Domain.Carts;
using GeekStore.Core.Factories;

namespace GeekStore.Cart.Data.Factories
{
    internal class CartItemFactory : IEntityFactory<CartItem, CartItemDTO>
    {
        public CartItem CreateEntity(CartItemDTO model)
        {
            return new CartItem(model.ProductId, model.ProductName, model.Quantity, model.Price);
        }

        public CartItemDTO CreateModel(CartItem entity)
        {
            return new CartItemDTO()
            {
                ProductId = entity.Id,
                ProductName = entity.Name,
                Quantity = entity.Quantity,
                Price = entity.Price
            };
        }
    }
}
