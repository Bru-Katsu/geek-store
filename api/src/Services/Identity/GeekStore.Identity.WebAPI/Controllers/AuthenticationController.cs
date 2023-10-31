using Microsoft.AspNetCore.Mvc;
using GeekStore.WebApi.Core.Controllers;
using GeekStore.Core.Notifications;
using MediatR;
using GeekStore.Identity.Application.Users.Commands;
using GeekStore.Identity.Application.Tokens.Commands;
using GeekStore.Identity.Application.Tokens.Queries;
using GeekStore.WebApi.Core.Models;
using GeekStore.Identity.Application.Tokens.ViewModels;
using GeekStore.Identity.WebAPI.ViewModels;

namespace GeekStore.Identity.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : MainController
    {
        private readonly IMediator _mediator;
        public AuthenticationController(INotificationService notificationService, IMediator mediator) : base(notificationService)
        {
            _mediator = mediator;
        }

        [HttpPost("new-account")]
        [ProducesResponseType(typeof(TokenResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel newUser)
        {
            if (!ModelState.IsValid)
                return CreateResponse(ModelState);

            var success = await _mediator.Send(new CreateUserCommand(newUser.Email, newUser.Password, newUser.Name, newUser.Surname, newUser.Cpf, newUser.Birthday));
            if (!success)
                return CreateResponse();

            var createRefreshTokenResult = await _mediator.Send(new CreateRefreshTokenCommand(newUser.Email));

            if(!createRefreshTokenResult.Succeeded)
                return CreateResponse();

            var tokenResult = await _mediator.Send(new GenerateTokenCommand(newUser.Email, createRefreshTokenResult.Data));

            return CreateResponse(tokenResult.Data);
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(TokenResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
                return CreateResponse(ModelState);

            var success = await _mediator.Send(new LoginCommand(loginModel.Email, loginModel.Password));
            if (!success)
                return Unauthorized(GetErrors());

            var createRefreshTokenResult = await _mediator.Send(new CreateRefreshTokenCommand(loginModel.Email));

            if (!createRefreshTokenResult.Succeeded)
                return CreateResponse();

            var tokenResult = await _mediator.Send(new GenerateTokenCommand(loginModel.Email, createRefreshTokenResult.Data));

            return CreateResponse(tokenResult.Data);
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(TokenResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Error>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestViewModel refreshToken)
        {
            var vm = await _mediator.Send(new RefreshTokenQuery()
            {
                Token = refreshToken.RefreshToken
            });

            if(vm == null)
            {
                AddValidationError("Refresh token expirado.");
                return Unauthorized(GetErrors());
            }

            var createRefreshTokenResult = await _mediator.Send(new CreateRefreshTokenCommand(vm.UserName));

            if (!createRefreshTokenResult.Succeeded)
                return CreateResponse(createRefreshTokenResult);

            var tokenResult = await _mediator.Send(new GenerateTokenCommand(vm.UserName, createRefreshTokenResult.Data));

            return CreateResponse(tokenResult.Data);
        }
    }
}