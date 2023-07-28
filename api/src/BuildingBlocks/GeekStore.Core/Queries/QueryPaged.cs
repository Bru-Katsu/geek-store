using GeekStore.Core.Models;

namespace GeekStore.Core.Queries
{
    public abstract class QueryPaged<TResult> : QuerySorted<Page<TResult>>, IQueryPaged<TResult>
    {
        private int? _pageIndex;
        private int? _pageSize;

        protected QueryPaged() { }

        public QueryPaged(int pageIndex, int pageSize, IEnumerable<SorterDescriptor> sortings) : base(sortings)
        {
            PageIndex = pageIndex - 1;
            PageSize = pageSize;
        }

        public int PageIndex { get => _pageIndex.HasValue ? _pageIndex.Value : 0; set => _pageIndex = value - 1; }
        public int PageSize { get => _pageSize.HasValue ? _pageSize.Value : int.MaxValue; set => _pageSize = value; }
    }
}
