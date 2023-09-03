using GeekStore.Core.Notifications;
using GeekStore.Core.Results;
using GeekStore.Customer.Application.Customers.Events;
using GeekStore.Customer.Domain.Customers.Repositories;
using GeekStore.Customer.Domain.Customers.Validators;
using MediatR;

namespace GeekStore.Customer.Application.Customers.Commands
{
    public class CustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<Guid>>,
                                          IRequestHandler<ChangeProfileImageCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;

        public CustomerCommandHandler(ICustomerRepository customerRepository, 
                                      INotificationService notificationService, 
                                      IMediator mediator)
        {
            _customerRepository = customerRepository;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return new FailResult<Guid>();
            }

            var customer = new Domain.Customers.Customer(request.UserId, request.Name, request.Surname, request.Birthday, request.Document, request.Email);
            if (!customer.IsValid())
            {
                _notificationService.AddNotifications(customer.ValidationResult);
                return new FailResult<Guid>();
            }

            var addValidator = new CustomerAddValidator(_customerRepository);
            var results = await addValidator.ValidateAsync(customer);
            if (!results.IsValid)
            {
                _notificationService.AddNotifications(results);
                return new FailResult<Guid>();
            }

            _customerRepository.Insert(customer);
            _customerRepository.SaveChanges();

            await _mediator.Publish(new CustomerCreatedEvent(customer));

            return new SuccessResult<Guid>(customer.Id);
        }

        public async Task Handle(ChangeProfileImageCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return;
            }

            var customer = await _customerRepository.GetById<Domain.Customers.Customer>(request.Id);
            if(customer == null)
            {
                _notificationService.AddNotification(nameof(customer.Id), "Cliente inexistente!");
                return;
            }

            customer.ChangeProfileImage(request.ProfileImage);

            if (!customer.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return;
            }

            _customerRepository.Update(customer);
            _customerRepository.SaveChanges();

            await _mediator.Publish(new CustomerProfileImageChangedEvent(customer));
        }
    }
}
