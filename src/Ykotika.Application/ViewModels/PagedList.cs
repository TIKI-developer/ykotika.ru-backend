using Microsoft.EntityFrameworkCore;

namespace Ykotika.Application.ViewModels
{
    public class PagedList<T> : BaseList<T>
    {
        private PagedList(List<T> items, int page, int pageSize, int totalCount) : base(items)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int? page, int? pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = query.ToList();
            
            if (page.HasValue && pageSize.HasValue)
            {
                items = await query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToListAsync();
            }
            else
            {
                page = 1;
                pageSize = totalCount;
            }

            return new(items, page.Value, pageSize.Value, totalCount);
        }
    }
}
