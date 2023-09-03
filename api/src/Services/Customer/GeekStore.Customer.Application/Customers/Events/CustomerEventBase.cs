using GeekStore.Core.Messages;

namespace GeekStore.Customer.Application.Customers.Events
{
    public abstract class CustomerEventBase : Event
    {
        public CustomerEventBase(Domain.Customers.Customer customer)
        {
            UserId = customer.Id;
            Name = customer.Name;
            Surname = customer.Surname;
            Birthday = customer.Birthday;
            Document = customer.Document;
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Document { get; set; }
    }
}
