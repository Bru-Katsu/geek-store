using GeekStore.Core.Models;
using GeekStore.Core.Notifications;
using GeekStore.WebApi.Core.Controllers;
using GeekStore.Website.Gateway.WebAPI.Models.Product;
using GeekStore.Website.Gateway.WebAPI.Services.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Website.Gateway.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : MainController
    {
        private readonly IProductApiService _productApi;

        public ProductController(INotificationService notificationService, IProductApiService productApi) : base(notificationService)
        {
            _productApi = productApi;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainNotification>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            var response = await _productApi.GetProduct(id);
            return Ok(response);
        }

        [HttpGet("all/")]
        [ProducesResponseType(typeof(Page<ProductsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainNotification>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProducts([FromQuery] AllProductsPageRequest request)
        {
            var page = await _productApi.GetAllProducts(request);
            return Ok(page);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainNotification>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostProduct(AddProductRequest request)
        {
            var response = await _productApi.AddProduct(request);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainNotification>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutProduct(UpdateProductRequest request)
        {
            var response = await _productApi.UpdateProduct(request);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<DomainNotification>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            await _productApi.DeleteProduct(id);
            return NoContent();
        }
    }
}
