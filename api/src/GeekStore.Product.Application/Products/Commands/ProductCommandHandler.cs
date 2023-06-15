using GeekStore.Core.Notifications;
using GeekStore.Core.UoW;
using GeekStore.Product.Domain.Products.Repositories;
using MediatR;

namespace GeekStore.Product.Application.Products.Commands
{
    public sealed class ProductCommandHandler : IRequestHandler<AddProductCommand>,
                                                IRequestHandler<UpdateProductCommand>,
                                                IRequestHandler<RemoveProductCommand>
    {
        private readonly INotificationService _notificationService;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;

        public ProductCommandHandler(INotificationService notificationService, IProductRepository productRepository, IUnitOfWork uow)
        {
            _notificationService = notificationService;
            _productRepository = productRepository;
            _uow = uow;
        }

        public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!request.IsValid())
                _notificationService.AddNotifications(request.ValidationResult);

            var entity = new Domain.Products.Product(request.Name, request.Price, request.Description, request.Category, request.ImageURL);

            _uow.BeginTransaction();

            _productRepository.Insert(entity);
            await _uow.CommitAsync();
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!request.IsValid())
                _notificationService.AddNotifications(request.ValidationResult);

            var entity = await _productRepository.GetById<Domain.Products.Product>(request.Id);
            if (entity == null)
            {
                await _uow.RollbackAsync();
                _notificationService.AddNotification(nameof(request.Id), "Produto não existe!");
                return;
            }

            entity.SetName(request.Name);
            entity.ChangePrice(request.Price);
            entity.SetDescription(request.Description);
            entity.ChangeCategory(request.Category);
            entity.ChangeImageUrl(request.ImageURL);

            _uow.BeginTransaction();
            _productRepository.Update(entity);
            await _uow.CommitAsync(cancellationToken);
        }

        public async Task Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!request.IsValid())
                _notificationService.AddNotifications(request.ValidationResult);

            _uow.BeginTransaction();

            var entity = await _productRepository.GetById<Domain.Products.Product>(request.Id);
            if (entity == null)
            {
                await _uow.RollbackAsync();
                _notificationService.AddNotification(nameof(request.Id), "Produto não existe!");
                return;
            }

            _productRepository.Delete(entity);
            await _uow.CommitAsync();
        }
    }
}
