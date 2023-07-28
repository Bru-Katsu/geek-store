namespace GeekStore.Core.Queries
{
    public abstract class QuerySorted<TResult> : IQuery<TResult>
    {
        protected QuerySorted() { }
        protected QuerySorted(IEnumerable<SorterDescriptor> sortings)
        {
            Sortings = sortings;
        }

        public IEnumerable<SorterDescriptor> Sortings { get; set; }
    }
}
