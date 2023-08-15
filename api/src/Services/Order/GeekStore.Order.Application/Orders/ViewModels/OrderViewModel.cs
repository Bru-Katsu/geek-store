using GeekStore.Order.Application.Orders.DTOs;

namespace GeekStore.Order.Application.Orders.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public AddressDTO Address { get; set; }
        public CouponDTO Coupon { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDiscount { get; set; }

        public IEnumerable<OrderItemDTO> Items { get; set; }
    }
}
