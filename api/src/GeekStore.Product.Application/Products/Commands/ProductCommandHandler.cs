using GeekStore.Core.Notifications;
using GeekStore.Core.Results;
using GeekStore.Product.Application.Products.Events;
using GeekStore.Product.Domain.Products.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Product.Application.Products.Commands
{
    public sealed class ProductCommandHandler : IRequestHandler<AddProductCommand, Result<Guid>>,
                                                IRequestHandler<UpdateProductCommand>,
                                                IRequestHandler<RemoveProductCommand>
    {
        private readonly INotificationService _notificationService;
        private readonly IProductRepository _productRepository;
        private readonly DbContext _context;
        private readonly IMediator _mediator;

        public ProductCommandHandler(INotificationService notificationService, 
                                     IProductRepository productRepository,
                                     IMediator mediator,
                                     DbContext context)
        {
            _notificationService = notificationService;
            _productRepository = productRepository;
            _mediator = mediator;
            _context = context;
        }

        public async Task<Result<Guid>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return new FailResult<Guid>();
            }

            var entity = new Domain.Products.Product(request.Name, request.Price, request.Description, request.Category, request.ImageURL);
            if (!entity.IsValid())
            {
                _notificationService.AddNotifications(entity.ValidationResult);
                return new FailResult<Guid>();
            }

            using var transaction = _context.Database.BeginTransaction();

            _productRepository.Insert(entity);
            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new ProductAddedEvent(entity));

            await transaction.CommitAsync(cancellationToken);

            return new SuccessResult<Guid>(entity.Id);
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return;
            }

            var entity = await _productRepository.GetById<Domain.Products.Product>(request.Id);
            if (entity == null)
            {
                _notificationService.AddNotification(nameof(request.Id), "Produto não existe!");
                return;
            }

            entity.SetName(request.Name);
            entity.ChangePrice(request.Price);
            entity.SetDescription(request.Description);
            entity.ChangeCategory(request.Category);
            entity.ChangeImageUrl(request.ImageURL);

            if (!entity.IsValid())
            {
                _notificationService.AddNotifications(entity.ValidationResult);
                return;
            }

            using var transaction = _context.Database.BeginTransaction();

            _productRepository.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new ProductUpdatedEvent(entity));

            await transaction.CommitAsync(cancellationToken);
        }

        public async Task Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return;
            }

            var entity = await _productRepository.GetById<Domain.Products.Product>(request.Id);
            if (entity == null)
            {
                _notificationService.AddNotification(nameof(request.Id), "Produto não existe!");
                return;
            }

            using var transaction = _context.Database.BeginTransaction();

            _productRepository.Delete(entity);
            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new ProductRemovedEvent(entity));

            await transaction.CommitAsync(cancellationToken);
        }
    }
}
