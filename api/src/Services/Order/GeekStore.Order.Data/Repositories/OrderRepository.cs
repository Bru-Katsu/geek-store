using GeekStore.Core.Data;
using GeekStore.Order.Data.Context;
using GeekStore.Order.Domain.Orders.Repositories;

namespace GeekStore.Order.Data.Repositories
{
    public class OrderRepository : Repository<OrderDataContext, Domain.Orders.Order>, IOrderRepository
    {
        public OrderRepository(OrderDataContext context) : base(context)
        { }
    }
}
