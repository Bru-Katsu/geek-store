using GeekStore.Cart.Data.DTOs;
using GeekStore.Cart.Domain.Carts.Repositories;
using GeekStore.Core.Factories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace GeekStore.Cart.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;
        private readonly IEntityFactory<Domain.Carts.Cart, CartDTO> _cartFactory;

        public CartRepository(IDistributedCache redisCache, 
                              IEntityFactory<Domain.Carts.Cart, CartDTO> cartFactory)
        {
            _redisCache = redisCache;
            _cartFactory = cartFactory;
        }

        public async Task<Domain.Carts.Cart?> GetCartAsync(Guid userId)
        {
            var cart = await _redisCache.GetStringAsync(userId.ToString());
            
            if (string.IsNullOrEmpty(cart))
                return default;

            var dto = JsonConvert.DeserializeObject<CartDTO>(cart);
            if(dto == null) 
                return default;

            return _cartFactory.CreateEntity(dto);
        }

        public async Task DeleteAsync(Domain.Carts.Cart cart)
        {
            await _redisCache.RemoveAsync(cart.Id.ToString());
        }

        public async Task SetAsync(Domain.Carts.Cart cart)
        {
            var content = JsonConvert.SerializeObject(_cartFactory.CreateModel(cart));
            await _redisCache.SetStringAsync(cart.Id.ToString(), content);
        }
    }
}
