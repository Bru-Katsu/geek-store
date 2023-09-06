﻿using GeekStore.Cart.Application.Cart.Queries;
using GeekStore.Cart.Application.Cart.ViewModels;
using GeekStore.Cart.Domain.Carts.Repositories;
using MediatR;

namespace GeekStore.Cart.Application.Cart.Commands
{
    public class CartQueryHandler : IRequestHandler<CartQuery, CartViewModel>
    {
        private readonly ICartRepository _cartRepository;

        public CartQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartViewModel> Handle(CartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCartAsync(request.UserId);

            if (cart == null)
                return default;

            return new CartViewModel
            {
                UserId = cart.Id,
                CouponId = cart.CouponId,
                Items = cart.Items.Select(item =>  new CartItemViewModel
                {
                    ProductId = item.Id,
                    ProductName = item.Name,
                    Quantity = item.Quantity,
                    Price = item.Price
                })
            };
        }
    }
}
