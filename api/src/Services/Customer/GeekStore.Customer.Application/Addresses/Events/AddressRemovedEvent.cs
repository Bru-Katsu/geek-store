using GeekStore.Customer.Domain.Customers;

namespace GeekStore.Customer.Application.Addresses.Events
{
    public class AddressRemovedEvent : CustomerAddressBaseEvent
    {
        public AddressRemovedEvent(Guid customerId, CustomerAddress customerAddress) : base(customerId, customerAddress)
        {
        }
    }
}
