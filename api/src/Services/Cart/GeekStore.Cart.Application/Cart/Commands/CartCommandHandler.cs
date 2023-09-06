﻿using GeekStore.Cart.Domain.Carts.Repositories;
using GeekStore.Core.Notifications;
using MediatR;

namespace GeekStore.Cart.Application.Cart.Commands
{
    public class CartCommandHandler : IRequestHandler<AddProductCartCommand>, 
                                      IRequestHandler<RemoveProductCartCommand>,
                                      IRequestHandler<ApplyCouponCartCommand>,
                                      IRequestHandler<RemoveCouponCartCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly INotificationService _notificationService;
        public CartCommandHandler(ICartRepository cartRepository, 
                                  INotificationService notificationService)
        {
            _cartRepository = cartRepository;
            _notificationService = notificationService;
        }

        public async Task Handle(AddProductCartCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return;
            }

            var cart = await _cartRepository.GetCartAsync(request.UserId);

            if (cart == null)
                cart = Domain.Carts.Cart.Factory.NewCart(request.UserId);

            cart.AddItem(new Domain.Carts.CartItem(request.ProductId, request.ProductName, request.ProductQuantity, request.ProductPrice));

            if (!cart.IsValid())
            {
                _notificationService.AddNotifications(cart.ValidationResult);
                return;
            }

            await _cartRepository.SetAsync(cart);
        }

        public async Task Handle(RemoveProductCartCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return;
            }

            var cart = await _cartRepository.GetCartAsync(request.UserId);
            if(cart == null)
            {
                _notificationService.AddNotification(nameof(request.UserId), "Carrinho não encontrado.");
                return;
            }

            cart.RemoveItem(request.ProductId);

            if (!cart.IsValid())
            {
                _notificationService.AddNotifications(cart.ValidationResult);
                return;
            }

            await _cartRepository.SetAsync(cart);
        }

        public async Task Handle(ApplyCouponCartCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return;
            }

            var cart = await _cartRepository.GetCartAsync(request.UserId);
            if (cart == null)
            {
                _notificationService.AddNotification(nameof(request.UserId), "Carrinho não encontrado.");
                return;
            }

            cart.SetCoupon(request.CouponId);

            if (!cart.IsValid())
            {
                _notificationService.AddNotifications(cart.ValidationResult);
                return;
            }

            await _cartRepository.SetAsync(cart);
        }

        public async Task Handle(RemoveCouponCartCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                _notificationService.AddNotifications(request.ValidationResult);
                return;
            }

            var cart = await _cartRepository.GetCartAsync(request.UserId);
            if (cart == null)
            {
                _notificationService.AddNotification(nameof(request.UserId), "Carrinho não encontrado.");
                return;
            }

            cart.UnsetCoupon();

            if (!cart.IsValid())
            {
                _notificationService.AddNotifications(cart.ValidationResult);
                return;
            }

            await _cartRepository.SetAsync(cart);
        }
    }
}
