using MediatR;

namespace GeekStore.Core.Queries
{
    public interface IQuery<TModel> : IRequest<TModel> { }
}
