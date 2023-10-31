namespace GeekStore.Customer.WebAPI.Requests
{
    public class AddAddressRequest
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int Type { get; set; }
    }
}
