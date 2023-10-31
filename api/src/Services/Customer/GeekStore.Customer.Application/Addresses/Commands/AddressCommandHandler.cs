using GeekStore.Core.Notifications;
using GeekStore.Core.Results;
using GeekStore.Customer.Application.Addresses.Events;
using GeekStore.Customer.Domain.Customers;
using GeekStore.Customer.Domain.Customers.Repositories;
using MediatR;

namespace GeekStore.Customer.Application.Addresses.Commands
{
    public class AddressCommandHandler : IRequestHandler<AddAddressCommand, Result<Guid>>,
                                         IRequestHandler<RemoveAddressCommand>
    {
        private readonly INotificationService _notificationService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;
        public AddressCommandHandler(INotificationService notificationService, 
                                     IMediator mediator, 
                                     ICustomerRepository customerRepository)
        {
            _notificationService = notificationService;
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        public async Task<Result<Guid>> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return new FailResult<Guid>();
            }

            var customer = await _customerRepository.GetById(request.CustomerId);
            if (customer == null)
            {
                _notificationService.AddNotification(nameof(customer.Id), "Cliente inexistente!");
                return new FailResult<Guid>();
            }

            var address = new CustomerAddress(request.Street, request.City, request.State, request.Country, request.ZipCode);
            if (!address.IsValid())
            {
                _notificationService.AddNotifications(address.ValidationResult);
                return new FailResult<Guid>();
            }

            customer.AddAddress(address);

            _customerRepository.Update(customer);
            _customerRepository.SaveChanges();

            await _mediator.Publish(new AddressAddedEvent(customer.Id, address));

            return new SuccessResult<Guid>(address.Id);
        }

        public async Task Handle(RemoveAddressCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return;
            }

            var customer = await _customerRepository.GetById(request.CustomerId);
            if (customer == null)
            {
                _notificationService.AddNotification(nameof(customer.Id), "Cliente inexistente!");
                return;
            }

            var address = customer.Addresses.FirstOrDefault(x => x.Id == request.AddressId);
            if(address is null)
            {
                _notificationService.AddNotification(nameof(request.AddressId), "Endereço não existe!");
                return;
            }

            customer.RemoveAddress(address);

            _customerRepository.Update(customer);
            _customerRepository.SaveChanges();

            await _mediator.Publish(new AddressRemovedEvent(customer.Id, address));
        }
    }
}
