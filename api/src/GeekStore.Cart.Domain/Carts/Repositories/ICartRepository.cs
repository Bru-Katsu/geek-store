namespace GeekStore.Cart.Domain.Carts.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartAsync(Guid userId);
        Task SetAsync(Cart cart);
        Task DeleteAsync(Cart cart);
    }
}
