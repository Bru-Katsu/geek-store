using GeekStore.Core.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekStore.Cart.Application.Cart.Commands
{
    public class CartCommandHandler : IRequestHandler<AddProdutoToCartCommand, Result<Guid>>
    {
        public Task<Result<Guid>> Handle(AddProdutoToCartCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
