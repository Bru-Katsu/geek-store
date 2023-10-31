using GeekStore.Core.Models;
using GeekStore.Core.Notifications;
using GeekStore.Customer.Application.Addresses.Commands;
using GeekStore.Customer.Application.Addresses.Queries;
using GeekStore.Customer.Application.Addresses.ViewModels;
using GeekStore.Customer.Application.Customers.Queries;
using GeekStore.Customer.Application.Customers.ViewModels;
using GeekStore.Customer.WebAPI.Requests;
using GeekStore.WebApi.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Customer.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AddressController : MainController
    {
        private readonly IMediator _mediator;

        public AddressController(INotificationService notificationService,
                                 IMediator mediator) : base(notificationService)
        {
            _mediator = mediator;
        }

        [HttpGet("{customerId}/addresses")]
        [ProducesResponseType(typeof(Page<CustomerAddressViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAddresses([FromRoute] Guid customerId)
        {
            var addressPage = await _mediator.Send(new CustomerAddressesQuery { CustomerId = customerId });

            return Ok(addressPage);
        }

        [HttpGet("{customerId}/addresses/{addressId}")]
        [ProducesResponseType(typeof(CustomerAddressViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAddress([FromRoute] Guid customerId, [FromRoute] Guid addressId)
        {
            var address = await _mediator.Send(new CustomerAddressQuery { CustomerId = customerId, AddressId = addressId });

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost("{customerId}/addresses")]
        [ProducesResponseType(typeof(CustomerAddressViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAddress([FromRoute] Guid customerId, [FromBody] AddAddressRequest request)
        {
            var result = await _mediator.Send(new AddAddressCommand(customerId, request.Street, request.City, request.State, request.Country, request.ZipCode, request.Type));

            if(!result.Succeeded)
                return CreateResponse(result);

            var addressId = result.Data;
            var address =  await _mediator.Send(new CustomerAddressQuery { AddressId = addressId, CustomerId = customerId });

            return Ok(address);
        }

        [HttpDelete("{customerId}/addresses/{addressId}")]        
        public async Task<IActionResult> RemoveAddress([FromRoute] Guid customerId, [FromRoute] Guid addressId)
        {
            await _mediator.Send(new RemoveAddressCommand(customerId, addressId));
            return NoContent();
        }
    }
}
