using GeekStore.Core.Models;

namespace GeekStore.Core.Queries
{
    public interface IQueryPaged<TResult> : IQuery<Page<TResult>>
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
    }
}
