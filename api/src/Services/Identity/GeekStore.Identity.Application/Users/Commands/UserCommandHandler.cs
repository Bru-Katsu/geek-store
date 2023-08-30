using GeekStore.Core.Notifications;
using GeekStore.Identity.Domain.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using GeekStore.Identity.Application.Users.Events;
using System.Security.Claims;

namespace GeekStore.Identity.Application.Users.Commands
{
    public class UserCommandHandler : IRequestHandler<LoginCommand, bool>,
                                      IRequestHandler<CreateUserCommand, bool>,
                                      IRequestHandler<DeleteUserByEmailCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;
        public UserCommandHandler(UserManager<User> userManager,
                                  SignInManager<User> signInManager,
                                  INotificationService notificationService,
                                  IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return false;
            }

            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: true);
            if(!result.Succeeded)
            {               
                if (result.IsLockedOut)
                    _notificationService.AddNotification("Erro", "Usuário temporariamente bloqueado por tentativas inválidas");
                else if(result.IsNotAllowed)
                    _notificationService.AddNotification("Erro", "O seu usuário foi bloqueado");
                else
                    _notificationService.AddNotification("Erro", "Usuário ou senha inválidos");
            }

            return result.Succeeded;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return false;
            }

            var exists = await _userManager.FindByEmailAsync(request.Email);
            if (exists != null)
            {
                _notificationService.AddNotification("Erro", "Já existe uma conta vinculada a esse e-mail");
                return false;
            }

            var user = new User(request.Email);
            var result = await _userManager.CreateAsync(user, request.Password);

            var claims = new []
            { 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, request.Name),
                new Claim(ClaimTypes.Surname, request.Surname),
                new Claim(ClaimTypes.DateOfBirth, $"{request.Birthday:yyyy-MM-dd}"),
            };

            await _userManager.AddClaimsAsync(user, claims);

            if (!result.Succeeded)
                _notificationService.AddNotifications(result.Errors.Select(x => new DomainNotification("Login", x.Description)));

            await _mediator.Publish(new UserCreatedEvent(request.Name, request.Surname, request.Document, request.Birthday, user));
            return !_notificationService.HasNotifications();
        }

        public async Task<bool> Handle(DeleteUserByEmailCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return false;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _notificationService.AddNotification("Erro", "Não existe uma conta vinculada a esse e-mail");
                return false;
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }
    }
}
