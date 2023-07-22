using GeekStore.Cart.Data.DTOs;
using GeekStore.Cart.Domain.Carts;
using GeekStore.Core.Factories;
namespace GeekStore.Cart.Data.Factories
{
    internal class CartFactory : IEntityFactory<Domain.Carts.Cart, CartDTO>
    {
        private readonly IEntityFactory<Domain.Carts.CartItem, CartItemDTO> _itemFactory;

        public CartFactory(IEntityFactory<CartItem, CartItemDTO> itemFactory)
        {
            _itemFactory = itemFactory;
        }

        public Domain.Carts.Cart CreateEntity(CartDTO model)
        {
            var items = model.Items.Select(_itemFactory.CreateEntity);
            return Domain.Carts.Cart.Factory.CreateWith(model.UserId, model.CouponId, items);
        }

        public CartDTO CreateModel(Domain.Carts.Cart entity)
        {
            return new CartDTO
            {
                UserId = entity.Id,
                CouponId = entity.CouponId,
                Items = entity.Items.Select(item => new CartItemDTO()
                {
                    ProductId = item.Id,
                    Price = item.Price,
                    ProductName = item.Name,
                    Quantity = item.Quantity,
                })
            };
        }
    }
}
