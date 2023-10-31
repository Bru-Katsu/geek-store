using GeekStore.Core.Models;
using GeekStore.Core.Notifications;
using GeekStore.WebApi.Core.Controllers;
using GeekStore.Website.Gateway.WebAPI.Models.Customer;
using GeekStore.Website.Gateway.WebAPI.Services.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Website.Gateway.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CustomerController : MainController
    {
        private readonly ICustomerApiService _customerApi;
        public CustomerController(INotificationService notificationService, ICustomerApiService customerApi) : base(notificationService)
        {
            _customerApi = customerApi;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var response = await _customerApi.GetCustomer(id);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Page<CustomersResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomers([FromQuery] AllCustomersPageRequest request)
        {
            var customer = await _customerApi.GetAllCustomers(request);
            return Ok(customer);
        }
    }
}
