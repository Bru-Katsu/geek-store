namespace GeekStore.Cart.Application.Cart.ViewModels
{
    public class CartViewModel
    {
        public Guid UserId { get; set; }
        public Guid? CouponId { get; set; }
        public IEnumerable<CartItemViewModel> Items { get; set; }
    }
}
