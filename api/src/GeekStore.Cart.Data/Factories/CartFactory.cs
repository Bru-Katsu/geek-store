using GeekStore.Cart.Data.DTOs;
using GeekStore.Core.Factories;
namespace GeekStore.Cart.Data.Factories
{
    internal class CartFactory : IEntityFactory<Domain.Carts.Cart, CartDTO>
    {
        private readonly IEntityFactory<Domain.Carts.CartItem, CartItemDTO> _itemFactory;
        public Domain.Carts.Cart CreateEntity(CartDTO model)
        {
            var items = model.Items.Select(_itemFactory.CreateEntity);
            return Domain.Carts.Cart.Factory.CreateWith(model.UserId, items);
        }

        public CartDTO CreateModel(Domain.Carts.Cart entity)
        {
            return new CartDTO
            {
                UserId = entity.UserId,
                Items = entity.Items.Select(item => new CartItemDTO()
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                })
            };
        }
    }
}
