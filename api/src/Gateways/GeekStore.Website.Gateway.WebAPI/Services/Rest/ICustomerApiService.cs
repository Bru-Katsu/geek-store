using GeekStore.Core.Models;
using GeekStore.Website.Gateway.WebAPI.Models.Customer;
using Refit;

namespace GeekStore.Website.Gateway.WebAPI.Services.Rest
{
    public interface ICustomerApiService
    {
        [Get("/customer/{id}")]
        Task<CustomerResponse> GetCustomer([AliasAs("id")] Guid customerId);

        [Get("/customer/users/{userId}")]
        Task<CustomerResponse> GetCustomerByUser([AliasAs("userId")] Guid userId);

        [Get("/customer")]
        Task<Page<CustomersResponse>> GetAllCustomers([Query] AllCustomersPageRequest request);

        [Get("/address/{customerId}/addresses/{addressid}")]
        Task<CustomerAddressResponse> GetAddress([AliasAs("customerId")] Guid customerId, [AliasAs("addressId")] Guid addressId);

        [Get("/address/{customerId}/addresses")]
        Task<Page<CustomerAddressResponse>> GetAddresses([AliasAs("customerId")] Guid customerId);

        [Post("/address/{customerId}/addresses")]
        Task<CustomerAddressResponse> CreateAddress([AliasAs("customerId")] Guid customerId, [Body] AddCustomerAddressRequest request);

        [Delete("/address/{customerId}/addresses/{addressId}")]
        Task DeleteAddress([AliasAs("customerId")] Guid customerId, [AliasAs("addressId")] Guid addressId);        
    }
}
