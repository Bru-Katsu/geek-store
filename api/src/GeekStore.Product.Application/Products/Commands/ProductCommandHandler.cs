using GeekStore.Core.Notifications;
using GeekStore.Product.Domain.Products.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Product.Application.Products.Commands
{
    public sealed class ProductCommandHandler : IRequestHandler<AddProductCommand>,
                                                IRequestHandler<UpdateProductCommand>,
                                                IRequestHandler<RemoveProductCommand>
    {
        private readonly INotificationService _notificationService;
        private readonly IProductRepository _productRepository;
        private readonly DbContext _context;

        public ProductCommandHandler(INotificationService notificationService, IProductRepository productRepository, DbContext context)
        {
            _notificationService = notificationService;
            _productRepository = productRepository;
            _context = context;
        }

        public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return;
            }

            var entity = new Domain.Products.Product(request.Name, request.Price, request.Description, request.Category, request.ImageURL);
            
            using var transaction = _context.Database.BeginTransaction();

            _productRepository.Insert(entity);
            _context.SaveChanges();

            await transaction.CommitAsync();
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

            using var transaction = _context.Database.BeginTransaction();

            _productRepository.Update(entity);
            _context.SaveChanges();

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

            using var transaction = _context.Database.BeginTransaction();

            var entity = await _productRepository.GetById<Domain.Products.Product>(request.Id);
            if (entity == null)
            {
                _notificationService.AddNotification(nameof(request.Id), "Produto não existe!");
                return;
            }

            _productRepository.Delete(entity);
            _context.SaveChanges();

            await transaction.CommitAsync();
        }
    }
}
