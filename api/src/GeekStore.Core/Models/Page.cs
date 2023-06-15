namespace GeekStore.Core.Models
{
    public class Page<T>
    {
        public Page(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
        }

        public IEnumerable<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
    }
}
