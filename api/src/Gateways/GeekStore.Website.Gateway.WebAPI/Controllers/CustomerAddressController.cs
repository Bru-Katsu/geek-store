using GeekStore.Core.Models;
using GeekStore.Core.Notifications;
using GeekStore.WebApi.Core.Controllers;
using GeekStore.WebApi.Core.User;
using GeekStore.Website.Gateway.WebAPI.Models.Customer;
using GeekStore.Website.Gateway.WebAPI.Services.Cache;
using GeekStore.Website.Gateway.WebAPI.Services.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Website.Gateway.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CustomerAddressController : MainController
    {
        private readonly ICustomerCacheService _customerCacheService;
        private readonly ICustomerApiService _customerApiService;
        private readonly IAspNetUser _aspNetUser;
        public CustomerAddressController(INotificationService notificationService,
                                         ICustomerApiService customerApiService,
                                         IAspNetUser aspNetUser,
                                         ICustomerCacheService customerCacheService) : base(notificationService)
        {
            _customerApiService = customerApiService;
            _aspNetUser = aspNetUser;
            _customerCacheService = customerCacheService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Page<CustomerAddressResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAddresses()
        {
            var userId = _aspNetUser.GetUserId();
            var customerId = await _customerCacheService.GetCustomerId(userId);
            var response = await _customerApiService.GetAddresses(customerId);
            return Ok(response);
        }

        [HttpGet("{addressId}")]
        [ProducesResponseType(typeof(CustomerAddressResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAddress([FromRoute] Guid addressId)
        {
            var userId = _aspNetUser.GetUserId();
            var customerId = await _customerCacheService.GetCustomerId(userId);
            var response = await _customerApiService.GetAddress(customerId, addressId);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerAddressResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAddress([FromBody] AddCustomerAddressRequest request)
        {
            var userId = _aspNetUser.GetUserId();
            var customerId = await _customerCacheService.GetCustomerId(userId);
            var response = await _customerApiService.CreateAddress(customerId, request);
            return Ok(response);
        }

        [HttpDelete("{addressId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid addressId)
        {
            var userId = _aspNetUser.GetUserId();
            var customerId = await _customerCacheService.GetCustomerId(userId);
            await _customerApiService.DeleteAddress(customerId, addressId);
            return NoContent();
        }
    }
}
