using GeekStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Core.Data.Extensions
{
    public static class EFCoreExtensions
    {
        public static async Task<Page<TReturn>> AsPagedAsync<TReturn, TEntity>(this IQueryable<TEntity> query, Func<TEntity, TReturn> factory, int pageIndex, int pageSize)
        {
            var count = await query.CountAsync();

            var pageItems = await query.Skip(pageIndex * pageSize)
                                       .Take(pageSize)
                                       .Select(item => factory(item))
                                       .ToListAsync();

            return new Page<TReturn>(pageItems, pageIndex + 1, pageSize, count);
        }
    }
}
