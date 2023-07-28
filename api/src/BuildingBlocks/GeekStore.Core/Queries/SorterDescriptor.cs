namespace GeekStore.Core.Queries
{
    public class SorterDescriptor
    {
        public SortingDirection Direction { get; set; }
        public string Field { get; set; }

        public enum SortingDirection
        {
            Ascending,
            Descending
        }
    }
}
