using GeekStore.Core.Queries;
using System.ComponentModel.DataAnnotations;

namespace GeekStore.Website.Gateway.WebAPI.Models.Base
{
    public class PagedRequest<T> where T : class
    {
        [Required]
        public int PageIndex { get; set; }

        [Required]
        public int PageSize { get; set; }
        public IEnumerable<SorterDescriptor> Sortings { get; set; } = new List<SorterDescriptor>();
    }
}
