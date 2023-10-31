namespace GeekStore.Customer.Application.Addresses.ViewModels
{
    public class CustomerAddressViewModel
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int Type { get; set; }
    }
}
