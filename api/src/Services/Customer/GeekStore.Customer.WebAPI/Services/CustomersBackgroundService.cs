using FluentValidation.Results;
using GeekStore.Core.Messages;
using GeekStore.Core.Messages.Integration;
using GeekStore.Core.Notifications;
using GeekStore.Customer.Application.Customers.Commands;
using GeekStore.MessageBus;
using MediatR;

namespace GeekStore.Customer.WebAPI.Services
{
    public class CustomersBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBus _bus;
        public CustomersBackgroundService(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetSubscribers()
        {
            _bus.RespondAsync<UserCreatedIntegrationEvent, ResponseMessage>(CreateCustomer);
            _bus.AdvancedBus.Connected += OnConnect;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetSubscribers();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> CreateCustomer(UserCreatedIntegrationEvent @event)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                var notificationService = scope.ServiceProvider.GetService<INotificationService>();

                var customerId = await mediator.Send(new CreateCustomerCommand(@event.Id, @event.Name, @event.Surname, @event.Birthday, @event.Document, @event.Email));
                var messages = notificationService.GetNotifications();

                var validationErrors = messages.Select(message => new ValidationFailure(message.Key, message.Message));

                return new ResponseMessage(new ValidationResult(validationErrors));
            }
        }
    }
}
