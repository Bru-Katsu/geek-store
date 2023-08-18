namespace GeekStore.Customer.Application.Customers.Events
{
    public class CustomerCreatedEvent : CustomerEventBase
    {
        public CustomerCreatedEvent(Domain.Customers.Customer customer) : base(customer)
        {
        }
    }
}
