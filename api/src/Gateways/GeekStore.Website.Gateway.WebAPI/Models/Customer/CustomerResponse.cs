namespace GeekStore.Website.Gateway.WebAPI.Models.Customer
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Document { get; set; }
    }
}
