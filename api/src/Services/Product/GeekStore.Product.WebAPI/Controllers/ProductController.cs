using GeekStore.Core.Models;
using GeekStore.Core.Notifications;
using GeekStore.Product.Application.Products.Commands;
using GeekStore.Product.Application.Products.Queries;
using GeekStore.Product.Application.Products.ViewModels;
using GeekStore.Product.WebAPI.ViewModels;
using GeekStore.WebApi.Core.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Product.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : MainController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator, INotificationService service) : base(service)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainNotification>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            var product = await _mediator.Send(new ProductQuery
            {
                Id = id
            });

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("all/")]
        [ProducesResponseType(typeof(Page<ProductsViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainNotification>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductListQuery query)
        {
            var page = await _mediator.Send(query);
            return Ok(page);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainNotification>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostProduct(AddProductViewModel viewModel)
        {
            var result = await _mediator.Send(new AddProductCommand(viewModel.Name, viewModel.Price, viewModel.Description, viewModel.Category, viewModel.ImageURL));
            if (!result.Succeeded)
                return CreateResponse();

            var product = await _mediator.Send(new ProductQuery
            {
                Id = result.Data
            });

            return Ok(product);
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainNotification>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutProduct(UpdateProductViewModel viewModel)
        {
            await _mediator.Send(new UpdateProductCommand(viewModel.Id, viewModel.Name, viewModel.Price, viewModel.Description, viewModel.Category, viewModel.ImageURL));
            var product = await _mediator.Send(new ProductQuery()
            {
                Id = viewModel.Id
            });

            return CreateResponse(product);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainNotification>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            await _mediator.Send(new RemoveProductCommand(id));
            return CreateResponse();
        }
    }
}
