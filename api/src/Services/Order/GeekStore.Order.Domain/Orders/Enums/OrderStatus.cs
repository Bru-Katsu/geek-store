namespace GeekStore.Order.Domain.Orders.Enums
{
    public enum OrderStatus
    {
        Pending = 0,
        Authorized = 1,
        Paid = 2,
        Refused = 3,
        Delivered = 4,
        Canceled = 5
    }
}
