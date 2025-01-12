using Microsoft.EntityFrameworkCore;

namespace Ykotika.Application.ViewModels
{
    public class BaseList<T>
    {
        public List<T> Items { get; private set; }

        protected BaseList(List<T> items)
        {
            Items = items;
        }

        public static async Task<BaseList<T>> CreateAsync(IQueryable<T> query)
        {
            var items = await query.ToListAsync();

            return new(items);
        }
    }
}
