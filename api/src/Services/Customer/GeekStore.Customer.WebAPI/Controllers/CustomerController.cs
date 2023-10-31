using GeekStore.Core.Models;
using GeekStore.Core.Notifications;
using GeekStore.Customer.Application.Customers.Queries;
using GeekStore.Customer.Application.Customers.ViewModels;
using GeekStore.WebApi.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Customer.WebAPI.Controllers
{
    [Authorize]
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

        [HttpGet("users/{userId}")]
        [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerByUserId(Guid userId)
        {
            var customer = await _mediator.Send(new CustomerByUserQuery()
            {
                UserId = userId
            });

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Page<CustomerListViewModel>), StatusCodes.Status200OK)]        
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerListQuery query)
        {
            var customer = await _mediator.Send(query);
            return Ok(customer);
        }
    }
}
