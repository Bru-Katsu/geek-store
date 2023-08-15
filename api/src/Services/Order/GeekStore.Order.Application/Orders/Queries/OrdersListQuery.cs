using GeekStore.Core.Queries;
using GeekStore.Order.Application.Orders.ViewModels;

namespace GeekStore.Order.Application.Orders.Queries
{
    public class OrdersListQuery : QueryPaged<OrderListViewModel>
    {
        public Guid? UserId { get; set; }
    }
}
