using GeekStore.Core.Messages;
using GeekStore.Customer.Domain.Customers;

namespace GeekStore.Customer.Application.Addresses.Events
{
    public abstract class CustomerAddressBaseEvent : Event
    {
        public CustomerAddressBaseEvent(Guid customerId, CustomerAddress customerAddress)
        {
            AggregateId = customerId;
            Street = customerAddress.Street;
            City = customerAddress.City;
            State = customerAddress.State;
            Country = customerAddress.Country;
            ZipCode = customerAddress.ZipCode;
            Type = (int)customerAddress.Type;
        }
        
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int Type { get; set; }
    }
}
