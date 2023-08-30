namespace GeekStore.Customer.Application.Customers.Events
{
    public class CustomerProfileImageChangedEvent : CustomerEventBase
    {
        public CustomerProfileImageChangedEvent(Domain.Customers.Customer customer) : base(customer)
        { }
    }
}
