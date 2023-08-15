using GeekStore.Core.Messages;
using GeekStore.Order.Domain.Orders.Enums;

namespace GeekStore.Order.Application.Orders.Events
{
    public abstract class OrderBaseEvent : Event
    {
        public OrderBaseEvent(Domain.Orders.Order entity)
        {
            AggregateId = entity.Id;
            UserId = entity.UserId;
            CreationDate = entity.CreationDate;
            Status = entity.Status;
            Total = entity.Total;
            TotalDiscount = entity.TotalDiscount;
        }

        public Guid UserId { get; private set; }
        public DateTime CreationDate { get; private set; }

        public OrderStatus Status { get; private set; }

        public decimal Total { get; private set; }
        public decimal TotalDiscount { get; private set; }
    }
}
