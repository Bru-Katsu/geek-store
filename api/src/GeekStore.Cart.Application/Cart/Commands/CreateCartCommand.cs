using GeekStore.Core.Messages;
using GeekStore.Core.Results;

namespace GeekStore.Cart.Application.Cart.Commands
{
    public class AddProdutoToCartCommand : Command<Result<Guid>>
    {
        public AddProdutoToCartCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
        public Guid ProductId { get; }
    }
}
