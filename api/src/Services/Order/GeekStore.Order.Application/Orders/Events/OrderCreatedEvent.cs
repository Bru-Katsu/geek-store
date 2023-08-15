namespace GeekStore.Order.Application.Orders.Events
{
    public class OrderCreatedEvent : OrderBaseEvent
    {
        public OrderCreatedEvent(Domain.Orders.Order order) : base(order)
        { }
    }
}
