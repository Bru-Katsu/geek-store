using GeekStore.Core.Notifications;
using GeekStore.Customer.Application.Customers.Queries;
using GeekStore.Customer.Application.Customers.ViewModels;
using GeekStore.WebApi.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Customer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : MainController
    {
        private readonly IMediator _mediator;

        public CustomerController(INotificationService notificationService, 
                                  IMediator mediator) : base(notificationService)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _mediator.Send(new CustomerQuery()
            {
                Id = id
            });

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status200OK)]        
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerListQuery query)
        {
            var customer = await _mediator.Send(query);
            return Ok(customer);
        }
    }
}
