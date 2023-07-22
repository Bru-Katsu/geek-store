using GeekStore.Cart.Application.Cart.ViewModels;
using GeekStore.Core.Queries;

namespace GeekStore.Cart.Application.Cart.Queries
{
    public class CartQuery : IQuery<CartViewModel>
    {
        public Guid UserId { get; set; }
    }
}
