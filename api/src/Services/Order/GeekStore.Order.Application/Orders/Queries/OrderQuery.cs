using GeekStore.Core.Queries;
using GeekStore.Order.Application.Orders.ViewModels;

namespace GeekStore.Order.Application.Orders.Queries
{
    public class OrderQuery : IQuery<OrderViewModel>
    {
        public Guid OrderId { get; set; }
    }
}
