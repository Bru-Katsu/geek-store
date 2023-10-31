using GeekStore.Customer.Domain.Customers;

namespace GeekStore.Customer.Application.Addresses.Events
{
    public class AddressAddedEvent : CustomerAddressBaseEvent
    {
        public AddressAddedEvent(Guid customerId, CustomerAddress customerAddress) : base(customerId, customerAddress)
        {
        }
    }
}
